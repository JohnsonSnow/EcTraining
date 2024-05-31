using Application;
using Infrastructure;
using Presentation;
using Serilog;
using Web.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Presentation.DependencyInjection).Assembly;


builder.Services.AddControllers().AddApplicationPart(assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
.AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", builder =>
{
    builder
    .WithOrigins("https://localhost:3001", "http://localhost:3001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.UseCors("CorsPolicy");


app.MapControllers();

app.Run();
