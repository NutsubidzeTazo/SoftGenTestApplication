using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using SoftGen.Repository.Repositories;
using SoftGen.Services.BusinessServices;
using SoftGen.Services.Interfaces;
using SoftGen.Services.UserServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<SoftGenApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SoftGenConnectionString")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new
      SymmetricSecurityKey
      (Encoding.UTF8.GetBytes
      (builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped<IBaseRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IBaseRepository<Teacher>, TeacherRepository>();
builder.Services.AddScoped<IBaseRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IBaseRepository<TeacherCourse>, TeacherCourseRepository>();
builder.Services.AddScoped<IBaseRepository<StudentCourse>, StudentCourseRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseSession();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
