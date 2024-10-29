namespace FinalProject
{
    public class TokenOptions
    {
        public required string Secret { get; set; }
        public int ExpiryDays { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
