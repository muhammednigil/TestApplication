namespace TestApplicationDomain.Entities
{
    public class RoomType : AuditEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}


