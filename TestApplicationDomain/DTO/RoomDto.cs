namespace TestApplicationDomain.DTO
{
    public class RoomDto : BaseDto
    {
        public string Code { get; set; }
        public RoomTypeDto RoomType { get; set; }
        public string Description { get; set; }
        public HotelDto Hotel { get; set; }
        public int RoomLimit { get; set; }
    }

}


