using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<IQueryable<Rating>> GetHotelRating(string hotelId);
    }
}
