using System;

namespace TestApplicationDomain.DTO
{
    public class BaseDto
    {
        public string ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
