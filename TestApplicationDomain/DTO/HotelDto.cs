using System.Collections.Generic;
using System.Text;

namespace TestApplicationDomain.DTO
{
    public class HotelDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CityDto City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public double FinalRating { get; set; }

    }
}


