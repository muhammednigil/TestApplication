using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.DTO;
using TestApplicationInterface.Service;

namespace TestApplication.Controllers
{
    public class BookingController : TestApplicationControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService) : base()
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostBooking([FromBody] BookingDto booking)
        {
            try
            {
                return CreatedServiceResponse(await _bookingService.SaveBooking(booking), "Booking added sucessfully");
            }
            catch (FluentValidation.ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }
    }
}
