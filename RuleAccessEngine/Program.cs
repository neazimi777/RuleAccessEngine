using RuleAccessEngine.DomainService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServiceRegisteration(builder.Configuration);
builder.Services.ConfigureServiceRegisteration(builder.Configuration);


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

app.Run();

