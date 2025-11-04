using Common.Domain;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Domain.RoleAgg
{
    public class RolePermission : BaseEntity
    {
        public long RoleId { get; private set; }
        public  Permission Permission { get; private set; }
    }
}
