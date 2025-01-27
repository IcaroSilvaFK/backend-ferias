using backend.src.Database;
using backend.src.Application.Services;
using backend.src.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using FluentValidation;
using backend.src.Dtos;
using backend.src.Validators;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll", Policy =>
    {
        Policy.AllowAnyHeader();
        Policy.AllowAnyMethod();
        Policy.AllowAnyOrigin();
    });
});
builder.Services.AddScoped<IValidator<UserDTO>, UserDTOValidator>();
builder.Services.AddScoped<IValidator<TaskDTO>, TaskDTOValidator>();
builder.Services.AddDbContext<Persistence>(options => options.UseSqlite("DataSource=backend.db; Cache=Shared"));
builder.Services.AddTransient<ITaskServiceInterface, TaskService>();
builder.Services.AddTransient<IAuthServiceInterface, AuthService>();
builder.Services.AddTransient<IUserServiceInterface, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "backend",
            ValidAudience = "",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e5266e9f-6aa0-4282-84f8-4657e2762050!"))
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

