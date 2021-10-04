using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<IQueryable<Room>> GetHotelRooms(string hotelId)
        {
            return Task.Run(() => Get(x => x.Hotel.Id == hotelId));
        }
    }
}
