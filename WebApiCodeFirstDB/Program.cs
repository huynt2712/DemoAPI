using Microsoft.EntityFrameworkCore;
using BlogWebApi.Configuration;
using BlogWebApi.Data;
using BlogWebApi.Services;
using BlogWebApi.Services.Interface;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.Configure<Course>(
    builder.Configuration.GetSection("Course"));

builder.Services.AddDbContext<BlogDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnection")));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() 
    || app.Environment?.EnvironmentName == "Development_Huy"
    || app.Environment?.EnvironmentName == "Development_Long")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});

//Map  C:\Users\long.nguyendh.STS\Desktop\DemoAPI\WebApiCodeFirstDB\ +Resources
//https://localhost:7213/  +Resources

//host web => Folder wwwroot

app.Run();
