using Coravel.Invocable;
using FantasyCrypto.Services.Interfaces;

namespace FantasyCrypto.Services;

public class StartupDataService(ICoinGeckoApiService coinGeckoApiService, ICoinGeckoService coinGeckoService) : IStartupDataService, IInvocable
{
    public async Task SeedCoinsAsync(CancellationToken token = default)
    {
        var existingCoins = await coinGeckoService.DataExistAsync(token).ConfigureAwait(false);

        if (!existingCoins)
        {
            var results = await coinGeckoApiService.GetAllCoinsAsync(token).ConfigureAwait(false);

            if (results != null)
                await coinGeckoService.UpdateOrCreateManyAsync(results, token);
        }
    }
    
    public async Task Invoke()
    {
        var results = await coinGeckoApiService.GetAllCoinsAsync();
        
        if (results != null)
        {
            await coinGeckoService.UpdateOrCreateManyAsync(results);
        }
    }
}