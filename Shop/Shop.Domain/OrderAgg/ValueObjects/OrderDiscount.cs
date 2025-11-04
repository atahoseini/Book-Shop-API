using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.ValueObjects
{
    public class OrderDiscount : ValueObject
    {
        public OrderDiscount(string discontTitle, int discountAmount)
        {
            DiscontTitle=discontTitle;
            DiscountAmount=discountAmount;
        }

        public string DiscontTitle { get; private set; }
        public int DiscountAmount { get; private  set; }
    }
}
