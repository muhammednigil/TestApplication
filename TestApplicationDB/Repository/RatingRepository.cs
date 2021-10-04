using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
        public Task<IQueryable<Rating>> GetHotelRating(string hotelId)
        {
            return Task.Run(() => Get(x => x.Hotel.Id == hotelId).OrderByDescending(o => o.CreatedDate).AsQueryable());
        }
    }
}
