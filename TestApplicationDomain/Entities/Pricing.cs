namespace TestApplicationDomain.Entities
{
    public class Pricing : AuditEntity<string>
    {
        public string Code { get; set; }
        public Room  Room { get; set; }
        public double  BasePrice { get; set; }
    }

}


