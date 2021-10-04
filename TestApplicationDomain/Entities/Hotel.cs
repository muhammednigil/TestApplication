using System.Collections.Generic;
using System.Text;

namespace TestApplicationDomain.Entities
{
    public class Hotel : AuditEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public double FinalRating { get; set; }

    }

}


