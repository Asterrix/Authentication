using Application;
using Infrastructure;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddTransient<ValidationExceptionMiddleware>();
builder.Services.AddTransient<ArgumentExceptionMiddleware>();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

// Add services to the container.
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

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseMiddleware<ArgumentExceptionMiddleware>();

app.MapControllers();

app.Run();
