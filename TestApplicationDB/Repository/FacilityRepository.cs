using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DbFactory dbFactory) : base(dbFactory)
        {
            
        }
        public Task<IQueryable<Facility>> GetHotelFacility(string hotelId)
        {
            return Task.Run(() => Get(x => x.Hotel.Id == hotelId));
        }
    }
}
