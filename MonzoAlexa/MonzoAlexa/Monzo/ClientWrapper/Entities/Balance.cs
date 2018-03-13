using Newtonsoft.Json;

namespace MonzoAlexa.Monzo.ClientWrapper.Entities
{
    public class Balance
    {
        [JsonProperty(PropertyName = "balance")]
        public int BalanceAmount { get; set; }

        [JsonProperty(PropertyName = "total_balance")]
        public int TotalBalance { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "spend_today")]
        public int SpendToday { get; set; }

    }
}
