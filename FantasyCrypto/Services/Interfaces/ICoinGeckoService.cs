using FantasyCrypto.Core.Dtos;

namespace FantasyCrypto.Services.Interfaces;

public interface ICoinGeckoService
{
    Task<bool> DataExistAsync(CancellationToken token = default);
    Task UpdateOrCreateManyAsync(List<CoinGeckoCoinDto> coins, CancellationToken token = default);
    Task<List<CoinGeckoCoinDto>> GetAllAsync();
}
