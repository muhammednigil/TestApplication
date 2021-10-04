using System;

namespace TestApplicationDomain.DTO.Request
{
    public class HotelRequest
    {
        public string HotelId { get; set; }
        public string RoomId { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

    }

}


