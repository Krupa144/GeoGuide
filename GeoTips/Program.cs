using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection; 
using GeoTips.Models; 

var builder = WebApplication.CreateBuilder(args);


// Add controlers 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GeoTips API",
        Version = "v1",
        Description = "API do nauki geografii i map świata"
    });
    
});

// CORS
builder.Services.AddCors(options =>
    {
    options.AddPolicy("AllowSpecificOrigin",
    builder => builder.WithOrigins(
        "http://localhost:3000",       
        "http://localhost:5173",       
        "http://127.0.0.1:5173",      
        "https://localhost:7197",
        "http://localhost:5289"
    )
    .AllowAnyMethod()
    .AllowAnyHeader());
    });

var app = builder.Build();

// Middleware

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoTips API v1");
        c.RoutePrefix = string.Empty; //delete swagger from /swagger to /
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

// Serve static files from the "html" directory
app.MapFallbackToFile("html/index.html");


app.Run();