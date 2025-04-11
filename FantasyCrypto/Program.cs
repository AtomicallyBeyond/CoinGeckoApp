using Coravel;
using FantasyCrypto.Components;
using FantasyCrypto.Infrastructure.Context;
using FantasyCrypto.Services;
using FantasyCrypto.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddDbContext<DbContextCoinGecko>(options =>
    options.UseSqlite("Data Source=coin-gecko.db"));


builder.Services.AddScoped<ICoinGeckoApiService, CoinGeckoApiService>();
builder.Services.AddScoped<ICoinGeckoService, CoinGeckoService>();
builder.Services.AddScoped<IStartupDataService, StartupDataService>();

builder.Services.AddScoped<ThemeService>();
builder.Services.AddScheduler();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IStartupDataService>();
    await seeder.SeedCoinsAsync();
}

app.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<StartupDataService>()
        .DailyAtHour(0)
        .Zoned(TimeZoneInfo.Local)
        .PreventOverlapping("StartupDataService");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();