using System.Linq;
using Alexa.NET.Request;
using MonzoAlexa.Helpers;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetBalanceIntent : IIntent
    {
        private readonly IMonzoClient _monzoClient;

        public GetBalanceIntent(string accessToken)
        {
            _monzoClient = new MonzoClient(accessToken);
        }

        public string IntentName => "GetBalanceIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            var accounts = _monzoClient.GetAccounts().Result;

            var validAccount = accounts.First(x => !x.Closed);

            var balance = _monzoClient.GetBalance(validAccount).Result;

            var amountData = CurrencyHelper.GetAmountString(balance);
           

            var message = string.Empty;

            if (amountData.Major < 0 || amountData.Minor < 0)
            {
                message = $"Your account is overdrawn by {amountData.Amount}";
            }

            if (!string.IsNullOrEmpty(amountData.Amount))
            {
               message = $"There is {amountData.Amount} available in your account";
            }
            else if(amountData.Minor == 0 && amountData.Major == 0)
            {
                message = "There is no money available in your account.";
            }

            return message;
        }

        public bool ShouldEndSession => false;
    }
}
