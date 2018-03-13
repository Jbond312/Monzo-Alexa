using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MonzoAlexa.Monzo.ClientWrapper.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MonzoAlexa.Monzo.ClientWrapper
{
    public class MonzoClient : IMonzoClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _settings;

        public MonzoClient(string accessToken, string baseUri = "https://api.monzo.com/")
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri),
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            var response = await _client.GetAsync("accounts");
            
            var accountResponse = JsonConvert.DeserializeObject<AccountResponse>(await response.Content.ReadAsStringAsync(), _settings);

            return accountResponse.Accounts;
        }

        public async Task<int> GetBalance(Account account)
        {
            var response = await _client.GetAsync($"balance?account_id={account.Id}");

            var balance = JsonConvert.DeserializeObject<Balance>(await response.Content.ReadAsStringAsync(), _settings);

            return balance.BalanceAmount;
        }
    }
}
