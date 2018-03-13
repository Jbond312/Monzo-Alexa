using System;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;
using Newtonsoft.Json;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetBalanceIntent : IIntent
    {
        private readonly ILambdaLogger _log;
        private readonly IMonzoClient _monzoClient;

        public GetBalanceIntent(string accessToken, ILambdaLogger log)
        {
            _log = log;
            _log.LogLine($"Initialising MonzoClient with token: {accessToken}");
            _monzoClient = new MonzoClient(accessToken);
        }

        public string IntentName => "GetBalanceIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            _log.LogLine($"Getting accounts");
            var accounts = _monzoClient.GetAccounts().Result;

            var accountJson = JsonConvert.SerializeObject(accounts);

            _log.LogLine($"Got accounts: {accountJson}");

            var validAccount = accounts.First(x => !x.Closed);

            var balance = _monzoClient.GetBalance(validAccount).Result;

            var balanceParts = (balance / (double)100).ToString().Split('.');

            var major = Convert.ToInt32(balanceParts[0]);
            var minor = Convert.ToInt32(balanceParts[1]);

            string amount = string.Empty;

            if (major != 0)
            {
                amount += $"{major} pound";

                if (major > 1)
                {
                    amount += "s";
                }
            };

            if (major != 0 && minor != 0)
            {
                amount += $" and {minor} pence";
            }

            if (major == 0 && minor != 0)
            {
                amount = $"{minor} pence";
            }
           

            var message = string.Empty;

            if (major < 0 || minor < 0)
            {
                message = $"Your account is overdrawn by {amount}";
            }

            if (!string.IsNullOrEmpty(amount))
            {
               message = $"There is {amount} available in your account";
            }
            else if(minor == 0 && major ==0)
            {
                message = "There is no money available in your account.";
            }

            return message;
        }

        public bool ShouldEndSession => false;
    }
}
