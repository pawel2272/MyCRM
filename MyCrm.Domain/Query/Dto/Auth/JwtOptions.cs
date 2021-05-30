namespace MyCrm.Domain.Query.Dto.Auth
{
    public class JwtOptions
    {
        public string JwtKey { get; set; }
        public int JwtExpireMinutes { get; set; }
        public string JwtIssuer { get; set; }
    }
}