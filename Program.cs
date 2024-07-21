using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Controllers;
using TaskManager.Interface;
using TaskManager.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager", Version = "v1" });
});
// Dependency Injection for Repository and Interface
builder.Services.AddScoped<ITaskRepo, TaskData>();
builder.Services.AddScoped<IAsyncTask, AsyncTask>();
builder.Services.AddScoped<IUser, UserRepo>();
var configuration = builder.Configuration;
// For CORS (Cross-Origin Resource Sharing)
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.WithMethods();
            builder.AllowAnyHeader();
        });

    // To allow Specific Domain
    /* options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
        });*/
});
//Adding JWT token base authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey    = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))


        };
    });

//to AddAuthorization
builder.Services.AddAuthorizationBuilder()
                         .AddPolicy("TeamLeaderOnly", policy => policy.RequireRole("TeamLeader"))
 
                         .AddPolicy("TeamMemebers", policy => policy.RequireRole("TeamMemebrs"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagerAPI");
        
    });
    app.UseCors(MyAllowSpecificOrigins);
}

app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.MapTasksEndpoints();

app.Run();
