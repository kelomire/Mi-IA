using Backend.Interfaces;
using Backend.Services;
using Backend.Providers;
using Backend.Configurations;
using Backend.Memory;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MemoriaService>();

builder.Services.Configure<AIConfiguration>(
    builder.Configuration.GetSection("AI"));

builder.Services.AddHttpClient<OllamaProvider>();

builder.Services.AddSingleton<PromptService>();

builder.Services.AddScoped<IIAService, IAService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=lionia.db"));

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
