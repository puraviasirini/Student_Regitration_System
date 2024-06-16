// Import necessary namespaces
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StudentRegistraionAPI.Data.Interfaces;
using StudentRegistraionAPI.Data.Repositories;
using StudentRegistraionAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database
builder.Services.AddDbContext<StudentRegistrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentRegistrationConnectionString")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IFileUploadServiceRepository, FileUploadServiceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Services.GetRequiredService<IWebHostEnvironment>(); // Get the hosting environment

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

var uploadsDirectory = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
if (!Directory.Exists(uploadsDirectory))
{
    Directory.CreateDirectory(uploadsDirectory);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsDirectory),
    RequestPath = "/uploads"
});

app.MapControllers();

app.Run();
