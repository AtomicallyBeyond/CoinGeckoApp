using FantasyCrypto.Core.Dtos;
using FantasyCrypto.Services.Interfaces;

namespace FantasyCrypto.Services;

public class CoinGeckoApiService(IHttpClientFactory httpClientFactory) : ICoinGeckoApiService
{
    public async Task<List<CoinGeckoCoinDto>?> GetAllCoinsAsync(CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        const string url = "https://api.coingecko.com/api/v3/coins/list";

        try
        {
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<CoinGeckoCoinDto>>();
            
            Console.WriteLine($"CoinGecko API returned error: {response.StatusCode}");
            return null;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error fetching coin list: {e.Message}");
            return null;
        }
    }
}