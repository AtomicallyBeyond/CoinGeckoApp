using FantasyCrypto.Core.Dtos;

namespace FantasyCrypto.Components.Pages.ViewModels;

public class AllCryptoViewModel
{
    public bool IsSearching { get; set; }
    public string SearchText { get; set; } = "";
    
    public List<CoinGeckoCoinDto> CoinList { get; set; } = new();
    public List<CoinGeckoCoinDto> FilteredCoinList { get; set; } = new();
}