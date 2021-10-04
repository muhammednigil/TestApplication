using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class HotelImageRepository : Repository<HotelImage>, IHotelImageRepository
    {
        public HotelImageRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<IQueryable<HotelImage>> GetHotelImages(string hotelId)
        {
            return Task.Run(() => Get(x => x.Hotel.Id == hotelId));
        }
    }
}
