using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YouToob.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<YouToobDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder
        .WithOrigins(allowedOrigins) // Replace with your allowed origin or "*" for any origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Set to true if you need to allow credentials
    });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 16 * 1024; // 16kb limit for form data
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllers();

app.Run();
