namespace SampleProject.Common.Infrastructure.Helper
{
    public class AppSettings
    {
        public string JwtSymmatricKey { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationTimeInMinutes { get; set; }
    }
}