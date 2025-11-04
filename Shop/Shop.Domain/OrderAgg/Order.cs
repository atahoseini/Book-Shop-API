using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        private Order()
        {
            
        }

        public Order(long userId)
        {
            UserId = userId;
            Status = OrderStatus.Pending;
            Items = new List<OrderItem>();
        }

        public long UserId { get; set; }
        public OrderStatus Status { get; private set; }
        public OrderDiscount? Discount { get; private set; }    
        public OrderAddress? Address { get; private set; }
        public List<OrderItem> Items { get; private set; }
        //public int TotalPrice => Items.Sum(s => s.TotalPrice);
        public int ItemCount => Items.Count;
        public DateTime? LastUpdate { get; private set; }

        public ShippingMethod? ShippingMethod { get; private set; }


        public int TotalPrice
        {
            get
            {
                var totalPrice = Items.Sum(i => i.TotalPrice);
                if(ShippingMethod.ShippingCost > 0)
                {
                    totalPrice += ShippingMethod.ShippingCost;
                }
                if (Discount != null)
                {
                    totalPrice -= Discount.DiscountAmount;
                }
                return totalPrice;
            }
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(long itemId) {
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
            if (currentItem != null)
            {
                Items.Remove(currentItem);
            }
        }
        public void ChangeCountItem(long itemId, int newCount)
        {
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
            if (currentItem ==  null)
                throw new NullOrEmptyDomainDataException();

            currentItem.ChangeCount(newCount);
        }
         
        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            LastUpdate = DateTime.Now;
        }

        public void Checkout(OrderAddress orderAddress)
        {
            Address = orderAddress;


        }

    }

}
