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

// Register services
builder.Services.AddMemoryCache();
builder.Services.AddCors();

// - Singletons
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUsersService, UsersService>();
builder.Services.AddSingleton<IGamesService, GamesService>();

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
