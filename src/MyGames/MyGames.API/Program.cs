using MyGames.API.HostedServices;
using MyGames.Core.AppSettings;
using MyGames.Core.Services;
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

// - Singletons
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<TwitchLoginService>();
builder.Services.AddSingleton<GamesService>();

// Register http clients
builder.Services.AddHttpClient<GamesService>();
builder.Services.AddHttpClient<TwitchLoginService>();

builder.Services.AddHostedService<BackgroundJobService>();

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
