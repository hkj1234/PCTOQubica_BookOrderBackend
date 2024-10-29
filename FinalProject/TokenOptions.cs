namespace FinalProject
{
    public class TokenOptions
    {
        public string? Secret { get; set; }
        public int ExpiryDays { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
