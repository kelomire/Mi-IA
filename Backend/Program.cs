using Backend.Interfaces;
using Backend.Services;
using Backend.Providers;
using Backend.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configuración
builder.Services.Configure<AIConfiguration>(
    builder.Configuration.GetSection("AI"));

// Servicios
builder.Services.AddHttpClient<OllamaProvider>();

builder.Services.AddSingleton<PromptService>();

builder.Services.AddScoped<IIAService, IAService>();

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