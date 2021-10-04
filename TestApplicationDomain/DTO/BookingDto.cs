using System;

namespace TestApplicationDomain.DTO
{
    public class BookingDto : BaseDto
    {
        public RoomDto Room { get; set; }
        public UserDto User { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public double TotalPrice { get; set; }
        public double VATPrice { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int AdultGuestCOunt { get; set; }
        public int KidsGuestCOunt { get; set; }
        public int InfantGuestCOunt { get; set; }
    }

}


