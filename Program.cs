using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression();



// Register HttpClient for making external API calls
builder.Services.AddHttpClient();

// Enable CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// **Add Response Compression Here**
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});


var app = builder.Build();

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseCors(); // Apply CORS settings
app.UseAuthorization();
app.MapControllers();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ✅ Enable Swagger UI for testing API endpoints
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Number Classification API v1");
    });
}
app.Run();

