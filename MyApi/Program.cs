using System.Collections;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => { return Results.Ok("Hello World!"); });

app.MapGet("/{name}", (string name) => { return Results.Ok($"Hello {name}"); });

app.MapPost("/", (User user) => { return Results.Ok(user); });

app.Run();


public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
}