namespace TestApplicationDomain.DTO
{
    public class PricingDto : BaseDto
    {
        public string Code { get; set; }
        public RoomDto Room { get; set; }
        public double  BasePrice { get; set; }
    }

}


