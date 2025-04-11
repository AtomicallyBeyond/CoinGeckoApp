using FantasyCrypto.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FantasyCrypto.Infrastructure.Context;

public class DbContextCoinGecko(DbContextOptions<DbContextCoinGecko> options) : DbContext(options)
{
    public DbSet<CoinGeckoCoin> Coins => Set<CoinGeckoCoin>();
}