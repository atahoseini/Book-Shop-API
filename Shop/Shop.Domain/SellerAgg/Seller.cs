using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg
{
    public class Seller : AggregateRoot
    {
        public long UserId { get;private set; }
        public string ShopName { get;private set; }
        public string NationalCode { get;private set; }
        public SellerStatus Status { get;private set; }
        public DateTime? LastUpdate { get; private set; }
        public List<SellerInventory>  Inventories{ get;private set; }
        public DateTime  MyProperty { get; set; }

        private Seller()
        {

        }
        public Seller(long userId, string shopName, string nationalCode)
        {
            Guard(shopName, nationalCode);
            UserId=userId;
            ShopName=shopName;
            NationalCode=nationalCode;
            Status=SellerStatus.New;
            Inventories=new List<SellerInventory>();
        }
        public void ChangeStatus(SellerStatus status)
        {
            Status=status;
            LastUpdate=DateTime.Now;
        }

        public void Edit(string shopName, string nationalCode)
        {
            Guard(shopName, nationalCode);
            ShopName =shopName;
            NationalCode=nationalCode;
            LastUpdate=DateTime.Now;
        }
        
        public void Guard(string shopName, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));
            if(IranianNationalIdChecker.IsValid(nationalCode) == false)
                throw new InvalidDomainDataException(nameof(nationalCode));
        }

        public void AddInventory(SellerInventory inventory)
        {
            if(Inventories.Any(i => i.ProductId == inventory.ProductId))
                throw new InvalidDomainDataException("This Product Already Exists In Inventory"); 
            Inventories.Add(inventory);
        }

        public void EditInventory(SellerInventory sellerInventory)
        {
            var currentInventory = Inventories.FirstOrDefault(i => i.Id == sellerInventory.Id);
            if (currentInventory == null)
                throw new InvalidDomainDataException("Product Not Found In Inventory");
            Inventories.Remove(currentInventory);
            Inventories.Add(sellerInventory);
        }
        
        public void DeleteInventory(long inventoryId)
        {
            var currentInventory = Inventories.FirstOrDefault(i => i.Id == inventoryId);
            if (currentInventory == null)
                throw new InvalidDomainDataException("Product Not Found In Inventory");
            Inventories.Remove(currentInventory);
        }


    }
}
