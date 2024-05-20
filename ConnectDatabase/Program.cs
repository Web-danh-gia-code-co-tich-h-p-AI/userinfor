using ConnectDatabase.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient; // Import MySQL EF Core Extension

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Add DbContext and MySqlConnection to services
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))); // Specify MySQL server version here

builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(connectionString));

// Thêm CORS vào dịch vụ
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



// Sử dụng cấu
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Sử dụng CORS policy
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.UseAuthentication();
app.Run();

// Define your DbContext class
