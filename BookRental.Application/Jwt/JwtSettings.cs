namespace Application.Jwt;

public class JwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }
}
