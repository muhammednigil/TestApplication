namespace TestApplicationDomain.Entities
{
    public class User : AuditEntity<string>
    {
        public string Code { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }

}


