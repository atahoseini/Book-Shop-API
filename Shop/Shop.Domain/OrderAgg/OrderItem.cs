using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(long orderId, long inventoryId, int count, int price)
        {
            PriceGuard(price);
            CountGuard(count);
            OrderId=orderId;
            InventoryId=inventoryId;
            Count=count;
            Price=price;
        }

        public long OrderId { get; internal set; }
        public long InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }

        public int TotalPrice => Count * Price;
        public void ChangeCount(int newCount)
        {
            CountGuard(newCount);
             Count = newCount;
        }
        public void SetPrice(int newPrice)
        {
            PriceGuard(newPrice);
            Price = newPrice;
        }
        public void PriceGuard(int newPrice)
        {
            if (newPrice < 1)
                throw new InvalidDomainDataException("Price must be greater than 0");
        }
        public void CountGuard(int count)
        {
            if (count < 1)
                throw new InvalidDomainDataException("");
        }
    }


}
