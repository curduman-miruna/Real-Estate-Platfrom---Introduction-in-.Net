using RealEstatePlatform.Infrastructure;
using RealEstatePlatform.Application;
using RealEstatePlatform.Application.Models;
using RealEstatePlatform.Application.Contracts.Interfaces;
using WebAPI.Services;
using RealEstatePlatform.Identity;
using RealEstatePlatform.API.Utility;
using Microsoft.OpenApi.Models;
using RealEstatePlatform_ML;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPredictionEnginePool<RealEstatePlatform_Model.ModelInput, RealEstatePlatform_Model.ModelOutput>()
    .FromFile("RealEstatePlatform.Model.mlnet");
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Add services to the container.
builder.Services.AddInfrastructureToDI(builder.Configuration);
builder.Services.AddInfrastrutureIdentityToDI(builder.Configuration);
builder.Services.AddAplicationServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Real Estate Platform API",

    });

    c.OperationFilter<FileResultContentTypeOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Open");
app.UseAuthorization();

app.MapControllers();

app.Run();
