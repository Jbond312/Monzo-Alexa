using System.Linq;
using Alexa.NET.Request;
using Humanizer;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetAccountIntent : IIntent
    {
        private readonly IMonzoClient _monzoClient;

        public GetAccountIntent(string accessToken)
        {
            _monzoClient = new MonzoClient(accessToken);
        }

        public string IntentName => "GetAccountIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            var accounts = _monzoClient.GetAccounts().Result;

            var enabledAccounts = accounts.Where(x => !x.Closed).ToList();

            string message;

            if (enabledAccounts.Count > 1)
            {
                message = $"You have {enabledAccounts.Count} accounts with Monzo. ";

                for (var i = 0; i < enabledAccounts.Count; i++)
                {
                    var account = enabledAccounts[i];
                    var type = account.Type.Replace("uk_", "");
                    message += $"The {(i+1).ToOrdinalWords()} account is for {account.Description} and is a {type} card. ";
                }
            }
            else
            {
                var account = enabledAccounts.First();
                var type = account.Type.Replace("uk_", "");
                message = $"There is one account for {account.Description} which is a {type} card.";
            }

            return message;
        }

        public bool ShouldEndSession => false;
    }
}
