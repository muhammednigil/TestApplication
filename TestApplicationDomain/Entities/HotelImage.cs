using System;

namespace TestApplicationDomain.Entities
{
    public class HotelImage : AuditEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }

    }

}


