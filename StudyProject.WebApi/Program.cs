using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.Mapper;
using StudyProject.Application.Services;
using StudyProject.Domain.Validation;
using StudyProject.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers();
services.AddSwaggerGen();

services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

var config = new TypeAdapterConfig();
new RegisterMapper().Register(config);

services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();
services.AddTransient<TestService>();
services.AddValidatorsFromAssemblyContaining<TenantValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

app.Logger.LogInformation("App has been started!");