using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class PricingUserDiscountRepository : Repository<PricingUserDiscount>, IPricingUserDiscountRepository
    {
        public PricingUserDiscountRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
