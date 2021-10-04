using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
