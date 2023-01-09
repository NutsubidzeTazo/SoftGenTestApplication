using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using SoftGen.Repository.Repositories;
using SoftGen.Services.BusinessServices;
using SoftGen.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SoftGenApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SoftGenConnectionString")));

builder.Services.AddScoped<IBaseRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IBaseRepository<Teacher>, TeacherRepository>();
builder.Services.AddScoped<IBaseRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IBaseRepository<TeacherCourse>, TeacherCourseRepository>();
builder.Services.AddScoped<IBaseRepository<StudentCourse>, StudentCourseRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();