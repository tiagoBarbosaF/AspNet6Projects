using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureServices(builder);

var app = builder.Build();
LoadConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();

void LoadConfiguration(WebApplication webApplication)
{
    Configuration.JwtKey = webApplication.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = webApplication.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = webApplication.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration();
    webApplication.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;
}

void ConfigureAuthentication(WebApplicationBuilder webApplicationBuilder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    webApplicationBuilder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc(WebApplicationBuilder builder1)
{
    builder1.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
}

void ConfigureServices(WebApplicationBuilder webApplicationBuilder1)
{
    webApplicationBuilder1.Services.AddControllers();
    webApplicationBuilder1.Services.AddDbContext<BlogDataContext>();
    webApplicationBuilder1.Services.AddTransient<TokenService>();
    webApplicationBuilder1.Services.AddTransient<EmailService>();
}
