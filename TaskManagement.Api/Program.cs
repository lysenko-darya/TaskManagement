using TaskManagement.Api.Application;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
TaskManagementApplicationInstaller.RegisterApplication(builder.Services);
TaskManagementInfrastructureInstaller.RegisterInfrastructure(builder.Services, builder.Configuration);

builder.Services.AddControllers();


var app = builder.Build();

await app.RunMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
