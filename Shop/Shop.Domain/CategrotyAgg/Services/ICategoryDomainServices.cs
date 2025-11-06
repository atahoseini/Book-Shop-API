using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategrotyAgg.Services
{
    public interface ICategoryDomainServices
    {
        public bool IsSlugExist(string slug);
    }
}
