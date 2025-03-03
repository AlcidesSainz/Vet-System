using Microsoft.EntityFrameworkCore;
using Vet_System.Model;
using Vet_System.Services.Implementation;
using Vet_System.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(15);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(optionsCORS =>
     {
         optionsCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("x-total-count");
     });
});

builder.Services.AddTransient<IFileStorage, LocalFileStorage>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
