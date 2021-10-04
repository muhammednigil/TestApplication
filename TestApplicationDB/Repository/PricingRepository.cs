using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class PricingRepository : Repository<Pricing>, IPricingRepository
    {
        public PricingRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
