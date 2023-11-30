using UserManagement.Api.Configurations;
using UserManagement.Api.ExceptionHandler;
using UserManagement.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.ConfigureDependency();
builder.ConfigureSwagger();
builder.ConfigureAuthentication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.HandleException();
app.Run();
