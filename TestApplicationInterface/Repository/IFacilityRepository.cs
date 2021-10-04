using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Task<IQueryable<Facility>> GetHotelFacility(string hotelId);
    }
}
