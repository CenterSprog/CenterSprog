using System.Text;
using Application.ClientInterfaces;
using Application.DAOInterfaces;
using Application.gRPCClients;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddScoped<IUserLogic,UserLogic>();
builder.Services.AddScoped<IUserClient,UserClient>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped<IHomeworkLogic, HomeworkLogic>();
builder.Services.AddScoped<IHomeworkClient, HomeworkClient>();

builder.Services.AddScoped<ILessonLogic, LessonLogic>();
builder.Services.AddScoped<ILessonClient, LessonClient>();
builder.Services.AddScoped<IClassLogic, ClassLogic>();
builder.Services.AddScoped<IClassClient, ClassClient>();

builder.Services.AddHttpClient<ILessonService, LessonHttpClient>();
builder.Services.AddLogging(builder => builder
    .AddConsole()
);


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();