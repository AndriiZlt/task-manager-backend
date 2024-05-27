
namespace aspnetcore.ntier.DTO.DTOs
{
    public class TradeToReturn
    {

        public string symbol { get; set; }

        public Trade? trade { get; set; }


    }

    public class Trade
    {
        public string[] c { get; set; }
        public int i { get; set; }
        public float p { get; set; }
        public int s { get; set; }
        public string t { get; set; }
        public string x { get; set; }
        public string z { get; set; }
    }


}

/*"symbol": "GOOG",
  "trade": {
    "c": [
      "@"
    ],
    "i": 2401,
    "p": 176.355,
    "s": 114,
    "t": "2024-05-24T19:59:55.7210164Z",
    "x": "V",
    "z": "C"
  }
}*/
