using FolderManager.Application;
using FolderManager.Data;
using FolderManager.Data.Context;
using FolderManager.Domain.Entities;
using FolderManager.Identity;
using FolderManager.Identity.Helpers;
using FolderManager.Shared;
using FolderManager.WebApi.Extensions;
using FolderManager.WebApi.Filters;
using FolderManager.WebApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//Register configuration
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<FolderManagerDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnect"),
    b => b.MigrationsAssembly("FolderManager.WebApi")));
builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<FolderManagerDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddApplication();
builder.Services.AddInfrastructureData();
builder.Services.AddInfrastructureShared(configuration);
builder.Services.AddInfrastructureIdentity(configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddControllersWithViews(options => options.Filters.Add(new ApiExceptionFilter()));
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);



builder.Services.AddApiVersioningExtension();
builder.Services.AddVersionedApiExplorerExtension();
builder.Services.AddSwaggerGenExtension();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
