using Google.Apis.Auth.OAuth2;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/get-token", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    string serviceAccountJson = await reader.ReadToEndAsync();

    try
    {
        string token = await GetAccessToken(serviceAccountJson);
        return Results.Json(new { access_token = token });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

static async Task<string> GetAccessToken(string serviceAccountJson)
{
    GoogleCredential credential;
    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serviceAccountJson)))
    {
        credential = GoogleCredential.FromStream(stream)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
    }

    var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
    return accessToken;
}

app.Run();
