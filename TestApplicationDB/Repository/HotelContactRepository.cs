using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class HotelContactRepository : Repository<HotelContact>, IHotelContactRepository
    {
        public HotelContactRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<IQueryable<HotelContact>> GetHotelContacts(string hotelId)
        {
            return Task.Run(() => Get(x => x.Hotel.Id == hotelId));
        }
    }
}
