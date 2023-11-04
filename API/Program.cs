using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Services.Contract;
using API.Services.Implementation;
using API.Converter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudentsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
});

//Se registra los servicio en la fabrica
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IQualificationService, QualificationService>();

builder.Services.AddAutoMapper(typeof(StudentConverter), typeof(QualificationConverter));

//COORS
/*builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowedClient", app =>
        {
            app.AllowAnyOrigin()
            .WithOrigins("https://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    }
);*/

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
//app.UseCors("AllowedClient");
app.Run();
