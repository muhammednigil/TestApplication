namespace TestApplicationDomain.DTO
{
    public class RatingDto : BaseDto
    {
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public string Review { get; set; }
        public HotelDto Hotel { get; set; }

    }
}
