using System;
using Newtonsoft.Json;

namespace MonzoAlexa.Monzo.ClientWrapper.Entities
{
    public class Transaction
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        //TODO MERCHANT
        public string Notes { get; set; }
        //TODO METADATA
        [JsonProperty(PropertyName = "include_in_spending")]
        public bool IncludeInSpending { get; set; }
        public string Category { get; set; }
    }
}
