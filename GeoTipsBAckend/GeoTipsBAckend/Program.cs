using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Supabase;
using GeoTipsBackend.Repositories.Interfaces;
using GeoTipsBackend.Repositories.Implementations;
using GeoTipsBackend.Services.Interfaces;
using GeoTipsBackend.Services.Implementations;

internal class Program
{
    private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var supabaseUrl = builder.Configuration["Supabase:Url"];
        var supabaseKey = builder.Configuration["Supabase:Key"];
        var supabaseJwtSecret = builder.Configuration["Supabase:JwtSecret"];

        if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
        {
            throw new ArgumentNullException("Supabase URL or Key is not configured in appsettings.json");
        }

        if (string.IsNullOrEmpty(supabaseJwtSecret))
        {
            throw new ArgumentNullException("Supabase JWT Secret is not configured in appsettings.json");
        }

        builder.Services.AddSingleton(_ =>
        {
            var client = new Client(supabaseUrl, supabaseKey, new SupabaseOptions
            {
                AutoRefreshToken = true,
            });
            client.InitializeAsync().Wait();
            return client;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://127.0.0.1:5500")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                              });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseJwtSecret)),
                NameClaimType = "sub" 
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddControllers();

        builder.Services.AddScoped<IContinentRepository, ContinentRepository>();
        builder.Services.AddScoped<ILessonRepository, LessonRepository>();

        builder.Services.AddScoped<IContinentService, ContinentService>();
        builder.Services.AddScoped<ILessonService, LessonService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
