namespace TestApplicationDomain.Entities
{
    public class City : AuditEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
