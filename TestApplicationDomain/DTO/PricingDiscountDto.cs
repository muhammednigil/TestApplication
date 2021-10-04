using System;

namespace TestApplicationDomain.DTO
{
    public class PricingDiscountDto : BaseDto
    {
        public string Code { get; set; }
        public RoomDto Room { get; set; }
        public string DiscountType { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}


