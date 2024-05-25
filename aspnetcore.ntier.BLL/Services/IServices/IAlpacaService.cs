using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices
{
    public interface IAlpacaService
    {
        Task<string> GetAssetsAsync(string keyId, string secretKey);
        Task<string> GetAssetAsync(string keyId, string secretKey, string assetId);
        Task<string> GetPositionsAsync(string keyId, string secretKey);
        Task<string> GetTransactionsAsync(string keyId, string secretKey);
        Task<string> GetMonthBarsAsync(string keyId, string secretKey, string symbol);
        Task<string> GetAccountAsync(string keyId, string secretKey);
        Task<string> GetOrdersAsync(string keyId, string secretKey);
        Task<string> CreateOrdersAsync(string keyId, string secretKey, OrderDTO order);
        Task<string> ClosePositionAsync(string keyId, string secretKey, string asset_id);
        Task<string> GetTradesAsync(string keyId, string secretKey, string symbol);
        Task<string> GetLastBarAsync(string keyId, string secretKey , string symbol);
    }
}