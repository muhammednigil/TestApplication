using System.Collections;

namespace TestApplicationDomain.DTO.Response
{
    public class HotelResponse
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double HowFar { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public double FinalRating { get; set; }
        public double BasePrice { get; set; }
        public double Discount { get; set; }
        public double UserDiscount { get; set; }

    }

}


