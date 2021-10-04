namespace TestApplicationDomain.Entities
{
    public class HotelContact : AuditEntity<string>
    {
        public User User { get; set; }
        public Hotel Hotel { get; set; }
    }

}


