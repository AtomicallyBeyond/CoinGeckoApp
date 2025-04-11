namespace FantasyCrypto.Services.Interfaces;

public interface IStartupDataService
{
    Task SeedCoinsAsync(CancellationToken token = default);
}