using FantasyCrypto.Core.Dtos;
using FantasyCrypto.Core.Entities;
using FantasyCrypto.Infrastructure.Context;
using FantasyCrypto.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantasyCrypto.Services;

public class CoinGeckoService(DbContextCoinGecko dbContext) : ICoinGeckoService
{
    public async Task<bool> DataExistAsync(CancellationToken token = default)
    {
        return await dbContext.Coins.AnyAsync(token);
    }
    
    public async Task UpdateOrCreateManyAsync(List<CoinGeckoCoinDto> coins, CancellationToken token = default)
    {
        var existingIds = await dbContext.Coins
            .Select(x => x.CoinGeckoCoinId)
            .ToListAsync(token).ConfigureAwait(false);

        var updateIds = coins.Select(x => x.CoinGeckoCoinDbId).Intersect(existingIds).ToList();
        
        var updateEntities = await dbContext.Coins
            .Where(x => updateIds.Contains(x.CoinGeckoCoinId))
            .ToListAsync(token).ConfigureAwait(false);

        var joinedEntites = (from entity in updateEntities
            join coin in coins on entity.CoinGeckoCoinId equals coin.CoinGeckoCoinDbId
            select new
            {
                Coin = coin,
                entity = entity
            }).ToList();

        foreach (var joinedEntity in joinedEntites)
        {
            var entity = joinedEntity.entity;
            var coin = joinedEntity.Coin;

            entity.Name = coin.Name;
            entity.Symbol = coin.Symbol;
        }

        var createEntities = coins
            .Where(x => !existingIds.Contains(x.CoinGeckoCoinDbId))
            .Select(x => new CoinGeckoCoin()
            {
                CoinGeckoCoinId = x.CoinGeckoCoinDbId,
                Symbol = x.Symbol,
                Name = x.Name
            }).ToList();

        await dbContext.AddRangeAsync(createEntities, token);
        await dbContext.SaveChangesAsync(token);
    }
    
    public async Task<List<CoinGeckoCoinDto>> GetAllAsync()
    {
        return await dbContext.Coins
            .OrderBy(c => c.Name)
            .Select(c => new CoinGeckoCoinDto()
            {
                Id = c.Id,
                CoinGeckoCoinDbId = c.CoinGeckoCoinId,
                Symbol = c.Symbol,
                Name = c.Name
            })
            .ToListAsync().ConfigureAwait(false);
    }
}