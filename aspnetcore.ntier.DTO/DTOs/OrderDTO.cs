

namespace aspnetcore.ntier.DTO.DTOs;

public class OrderDTO
{
    public string Symbol { get; set; }
    public string Qty { get; set; }
    public string Side { get; set; }
    public string Type { get; set; }
    public string Limit_price { get; set; }
    public string Time_in_force { get; set; }

    public OrderDTO() 
    {
        Side = "buy";
        Type= "limit";
        Time_in_force = "gtc";
    }

}
