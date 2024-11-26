using Intoxalock.Regular.POC.Web.Apis;
using Intoxalock.Regular.POC.Web.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<RegulaOcrService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Intoxalock.Regula.Forensic.POC",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API Example v1");
    });
}

const string BaseUrl = "/regulaforensics-poc";
app
    .MapGroup(BaseUrl)
    .MapDLValidationApiEndpoints();

app.Run();
