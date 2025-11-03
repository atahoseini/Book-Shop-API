using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg.Services
{
    public interface IDomainUserServices
    {
        bool isEmailExisats(string email);
        bool PhoneNumberIsExist(string phoneNumber);
    }
}
