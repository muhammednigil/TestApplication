using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class PricingDiscountRepository : Repository<PricingDiscount>, IPricingDiscountRepository
    {
        public PricingDiscountRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
