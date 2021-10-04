using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.DTO;

namespace TestApplicationInterface.Service
{
    public interface IBookingService
    {
        Task<bool> SaveBooking(BookingDto booking);
    }
}
