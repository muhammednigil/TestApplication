namespace TestApplicationDomain.DTO
{
    public class CityDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CountryDto Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
