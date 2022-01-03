using MyGames.Core.AppSettings;
using MyGames.Core.Services;
using MyGames.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Map appSettings kvp to C# classes.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<TwitchLoginSettings>(builder.Configuration.GetSection("TwitchLogin"));

// Register services

// - Singletons
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<TwitchLoginService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
