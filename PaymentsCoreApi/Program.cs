using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentsCoreApi.Logic.Interfaces;
using PaymentsCoreApi.Logic.Implementations;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // load up serilog configuraton
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);
    builder.Services.AddDbContext<DataBaseContext>(options =>
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseMySQL(connectionString);
    });
    builder.Services.AddSwaggerExtension();
    builder.Services.AddControllersExtension();
    builder.Services.AddCorsExtension();
    builder.Services.AddHealthChecks();
    builder.Services.AddJWTAuthentication(builder.Configuration);
    builder.Services.AddAuthorizationPolicies(builder.Configuration);
    builder.Services.AddApiVersioningExtension();
    builder.Services.AddMvcCore()
        .AddApiExplorer();
    builder.Services.AddVersionedApiExplorerExtension();
    builder.Services.AddScoped<IAgentManagement, AgentManagement>();
    builder.Services.AddScoped<IAuthenticationManagement, AuthenticationManagement>();
    builder.Services.AddScoped<ICommonLogic, CommonLogic>();
    builder.Services.AddScoped<ICustomerManagement, CustomerManagement>();
    builder.Services.AddScoped<IDataManagement, DataManagement>();
    builder.Services.AddScoped<IHttpServices, HttpServices>();
    builder.Services.AddScoped<IUserAuthenticationManagement, UserAuthenticationManagement>();
    builder.Services.AddScoped<IProductManagement, ProductManagement>();
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerExtension();
    app.UseErrorHandlingMiddleware();
    app.UseHealthChecks("/health");
    app.MapControllers();
    app.Run();

    Log.Information("Application Starting");

}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}
finally
{
    Log.CloseAndFlush();
}
