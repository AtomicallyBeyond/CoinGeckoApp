using FantasyCrypto.Core.Dtos;

namespace FantasyCrypto.Services.Interfaces;

public interface ICoinGeckoApiService
{
    Task<List<CoinGeckoCoinDto>?> GetAllCoinsAsync(CancellationToken token = default);
}