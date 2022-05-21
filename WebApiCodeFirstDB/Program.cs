using Microsoft.EntityFrameworkCore;
using BlogWebApi.Configuration;
using BlogWebApi.Data;
using BlogWebApi.Services;
using BlogWebApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<IEmailService, EmailService>(); //declaration
//builder.Services.AddTransient<IEmailService, EmailNewService>(); //declaration
//builder.Services.AddSingleton<IEmailService, EmailService>(); //declaration
//builder.Services.AddScoped<IEmailService, EmailService>(); //declaration
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
//declare DI
builder.Services.Configure<Course>(
    builder.Configuration.GetSection("Course"));

builder.Services.AddDbContext<BlogDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnection")));

//
//builder.Configuration.GetSection("ConnectionStrings:BlogDbConnection").Value)


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment?.EnvironmentName == "Development_Long")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500/",
                                              "http://127.0.0.1:5501/");
                      });
});

app.UseCors(MyAllowSpecificOrigins);

app.Run();
