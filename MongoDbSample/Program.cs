using Microsoft.Extensions.Options;
using MongoDbSample.Models;
using MongoDbSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UserStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(UserStoreDatabaseSettings)));

builder.Services.AddSingleton<IUserStoreDatabaseSettings>(x =>
    x.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

