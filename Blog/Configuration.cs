namespace Blog;

public static class Configuration
{
    public static string JwtKey = "6ff989b6-c9f3-4de7-beb4-91e54e114142";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_597e73e7-3913-42b9-";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}