using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;


namespace aspnetcore.ntier.BLL.Services
{
    public class AlpacaService : IAlpacaService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AlpacaService> _logger;
        private readonly Uri tradingUri = new Uri("https://paper-api.alpaca.markets/v2/");
        private readonly Uri dataUri = new Uri("https://data.alpaca.markets/v2/"); 
        public AlpacaService(IMapper mapper, ILogger<AlpacaService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> GetAssetsAsync(string keyId, string secretKey
            )
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "assets").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetAssetAsync(string keyId, string secretKey, string assetId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + $"assets/{assetId}").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetPositionsAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "positions").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> ClosePositionAsync(string keyId, string secretKey, string asset_id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);

                var response = client.DeleteAsync(this.tradingUri + $"positions/{asset_id}" ).Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetTransactionsAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "account/activities").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetMonthBarsAsync(string keyId, string secretKey, string symbol)
        {
            using (var client = new HttpClient())
            {
                var end = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
                var start = DateTime.UtcNow.AddDays(-31).ToString("yyyy-MM-dd");
                Console.WriteLine($"start:{start}, end:{end}");
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.dataUri+$"stocks/{symbol}/bars?timeframe=1Day&start={start}&end={end}&limit=1000&feed=sip&sort=asc").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetAccountAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "account").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> GetOrdersAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "orders").Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

        public async Task<string> CreateOrdersAsync(string keyId, string secretKey, OrderDTO orderToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var orderJSON = JsonConvert.SerializeObject(orderToCreate);
                var payload = new StringContent(orderJSON, Encoding.UTF8, "application/json");  
                var response = client.PostAsync(this.tradingUri + "orders", payload).Result;
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

        }

    }
}