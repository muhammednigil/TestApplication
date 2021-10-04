namespace TestApplicationDomain.Entities
{
    public class RoomFacilityType : AuditEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


