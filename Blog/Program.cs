using System.IO.Compression;
using System.Text;
using System.Text.Json.Serialization;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication(builder);
ConfigureMvc(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
LoadConfiguration(app);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("\nDevelopment environment.\n");
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("v1/swagger.json", "Blog v1"));
}

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
    builder1.Services.AddMemoryCache();
    builder1.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
    });
    builder1.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
    });
    builder1.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        });
}

void ConfigureServices(WebApplicationBuilder webApplicationBuilder1)
{
    var connectionString = webApplicationBuilder1.Configuration.GetConnectionString("DefaultConnection");
    webApplicationBuilder1.Services.AddControllers();
    webApplicationBuilder1.Services.AddDbContext<BlogDataContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
    webApplicationBuilder1.Services.AddTransient<TokenService>();
    webApplicationBuilder1.Services.AddTransient<EmailService>();
}
