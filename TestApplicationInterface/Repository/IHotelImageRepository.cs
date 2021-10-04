using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IHotelImageRepository : IRepository<HotelImage>
    {
        Task<IQueryable<HotelImage>> GetHotelImages(string hotelId);
    }
}
