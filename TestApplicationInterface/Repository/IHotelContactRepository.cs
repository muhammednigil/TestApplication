using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IHotelContactRepository : IRepository<HotelContact>
    {
        Task<IQueryable<HotelContact>> GetHotelContacts(string hotelId);
    }
}
