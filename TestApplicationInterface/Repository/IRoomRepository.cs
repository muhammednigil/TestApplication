using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IQueryable<Room>> GetHotelRooms(string hotelId);
    }
}
