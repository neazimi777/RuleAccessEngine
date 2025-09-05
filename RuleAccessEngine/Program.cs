using RuleAccessEngine.DomainService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServiceRegisteration(builder.Configuration);
builder.Services.ConfigureServiceRegisteration(builder.Configuration);
builder.Host.UseSerilog();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.Run();

