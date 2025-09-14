using TaskManagement.Api.Application;
using TaskManagement.Api.Middleware;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
TaskManagementApplicationInstaller.RegisterApplication(builder.Services);
TaskManagementInfrastructureInstaller.RegisterInfrastructure(builder.Services, builder.Configuration);

var app = builder.Build();

await app.RunMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
