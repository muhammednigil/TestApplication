namespace TestApplicationDomain.Entities
{
    public class Facility : AuditEntity<string>
    {
        public string FacilityTypeCode { get; set; }
        public string Description { get; set; }
        public Hotel Hotel { get; set; }
    }
}


