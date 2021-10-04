using System;

namespace TestApplicationDomain.DTO
{
    public class HotelImageDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime ExpiryDate { get; set; }
        public HotelDto Hotel { get; set; }
        public RoomDto Room { get; set; }

    }
}


