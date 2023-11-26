using Application.ClientInterfaces;
using Application.DAOInterfaces;
using Application.gRPCClients;
using Application.Logic;
using Application.LogicInterfaces;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHeartbeatLogic, HeartbeatLogic>();
builder.Services.AddScoped<IHeartbeatDAO, HeartbeatClient>();

builder.Services.AddScoped<IHomeworkLogic, HomeworkLogic>();
builder.Services.AddScoped<IHomeworkClient, HomeworkClient>();

builder.Services.AddScoped<IClassLogic, ClassLogic>();
builder.Services.AddScoped<IClassClient, ClassClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();