namespace TestApplicationDomain.Entities
{
    public class Rating : AuditEntity<string>
    {
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public string Review { get; set; }
        public Hotel Hotel { get; set; }

    }
}
