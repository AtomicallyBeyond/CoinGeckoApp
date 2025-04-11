using System.ComponentModel.DataAnnotations;

namespace FantasyCrypto.Core.Entities;

public class CoinGeckoCoin
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string CoinGeckoCoinId { get; set; } = "";
    
    [MaxLength(100)]
    public string Symbol { get; set; } = "";
    
    [MaxLength(100)]
    public string Name { get; set; } = "";
}
