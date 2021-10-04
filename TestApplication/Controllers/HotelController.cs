using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain.DTO.Request;
using TestApplicationDomain.DynamicQuery;
using TestApplicationInterface.Service;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : TestApplicationControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService) : base()
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHotels(
            [FromHeader] DataDescriptor dataDescriptor,
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] DateTime checkInDate,
            [FromQuery] DateTime checkOutDate)
        {
            try
            {
                var email = "test@test.com"; //we can get if authorization is there
                var request = new HotelRequest
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    Email = email
                };
                var result = await _hotelService.GetHotelResponse(request, dataDescriptor);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return HandleUserException(ex);
            }
            catch (Exception ex)
            {
                return HandleOtherException(ex);
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHotelById(
            [FromRoute] string Id,
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] DateTime checkInDate,
            [FromQuery] DateTime checkOutDate)
        {

            try
            {
                var email = "test@test.com"; //we can get if authorization is there
                var request = new HotelRequest
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    Email = email,
                    HotelId = Id
                };
                return OkServiceResponse(await _hotelService.GetHotelDeatils(request));
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
