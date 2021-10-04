namespace TestApplicationDomain.Entities
{
    public class Room : AuditEntity<string>
    {
        public string Code { get; set; }
        public RoomType RoomType { get; set; }
        public string Description { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomLimit { get; set; }
    }

}


