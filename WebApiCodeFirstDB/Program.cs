using BlogApi.Services;
using BlogApi.Services.Interface;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(c => c.AllowAnyOrigin());

app.Run();
