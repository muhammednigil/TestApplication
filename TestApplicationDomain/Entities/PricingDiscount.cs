using System;

namespace TestApplicationDomain.Entities
{
    public class PricingDiscount : AuditEntity<string>
    {
        public string Code { get; set; }
        public Room Room { get; set; }
        public string DiscountType { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}


