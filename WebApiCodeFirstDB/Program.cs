using Microsoft.EntityFrameworkCore;
using BlogWebApi.Configuration;
using BlogWebApi.Data;
using BlogWebApi.Services;
using BlogWebApi.Services.Interface;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using BlogWebApi.ViewModel.User;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.Configure<Course>(builder.Configuration.GetSection("Course"));
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});
var secretKey = builder.Configuration["AppSetting:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            //Nguoi cap phat token
            ValidateIssuer = false, //indetity server 4, facebook, google
            //Nguoi su dung
            ValidateAudience = false,

            //Chu ky
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes), //Thuat toan,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddDbContext<BlogDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnection")));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5501", "http://127.0.0.1:5500")
                          .AllowCredentials()
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

app.MapControllers();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});

//Map  C:\Users\long.nguyendh.STS\Desktop\DemoAPI\WebApiCodeFirstDB\ +Resources
//https://localhost:7213/  +Resources

//host web => Folder wwwroot

app.Run();
