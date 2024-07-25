using System.Reflection;
using orchestrator.api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
        .AddEnvironmentVariables()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services
    .AddServiceConfiguration(builder.Configuration)
    .AddEventBus(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/ping", () => Results.Ok(DateTime.Now.ToShortDateString()))
.WithName("Check")
.WithOpenApi();

app.Run();