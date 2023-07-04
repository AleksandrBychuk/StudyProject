using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.Interfaces;
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

var config = TypeAdapterConfig.GlobalSettings;
config.Apply(new RegisterMapper());

services.AddSingleton(config);
services.AddScoped<IMapper, ServiceMapper>();
services.AddValidatorsFromAssemblyContaining<TenantValidator>();
services.AddFluentValidationClientsideAdapters();
services.AddTransient<ITenantService, TenantService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<IRoleService, RoleService>();
services.AddTransient<IPermissionService, PermissionService>();

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