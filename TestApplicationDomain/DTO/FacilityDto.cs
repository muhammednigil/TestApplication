namespace TestApplicationDomain.DTO
{
    public class FacilityDto : BaseDto
    {
        public string FacilityTypeCode { get; set; }
        public string Description { get; set; }
        public HotelDto Hotel { get; set; }
    }
}


