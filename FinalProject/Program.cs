using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FinalProject;
using FinalProject.Controllers.Customer;
using FinalProject.Controllers.Book;
using FinalProject.Database.Book.Entities;
using FinalProject.Database.Context;
using FinalProject.Controllers.Order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database SQL server
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

//database MySQL 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

// Configura JWT
// Questo serve per contorllare se JWT ?valido o no rispettando le seguente regole 
#pragma warning disable 8602, 8604
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });
#pragma warning restore 8602, 8604

//DE
builder.Services
    .AddCustomer()
    .AddBook()
    .AddContext()
    .AddOrder();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();