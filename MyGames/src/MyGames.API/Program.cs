using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyGames.Core.AppSettings;
using MyGames.Core.Repositories;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;
using MyGames.Database.Schemas;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Map appSettings kvp to C# classes.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<TwitchLoginSettings>(builder.Configuration.GetSection("TwitchLogin"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register services
builder.Services.AddMemoryCache();
builder.Services.AddCors();

JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Convert.FromBase64String(jwtSettings.SecretKey))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDbRepository<User>, MongoDbRepository<User>>();

builder.Services.AddSingleton<IUsersService, UsersService>();
builder.Services.AddSingleton<IGamesService, GamesService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();


var app = builder.Build();

try
{
    Log.Information("Starting up");
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();
    
    app.UseCors(b => b
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3000")
        .AllowCredentials());

    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.MapControllers();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex.Message);

    return 1;
}
finally
{
    Log.CloseAndFlush();
}
