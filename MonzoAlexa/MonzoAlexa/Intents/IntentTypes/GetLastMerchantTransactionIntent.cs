using System;
using System.Globalization;
using System.Linq;
using Alexa.NET.Request;
using Humanizer;
using MonzoAlexa.ExtensionMethods;
using MonzoAlexa.Helpers;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetLastMerchantTransactionIntent : IIntent
    {
        private readonly IMonzoClient _monzoClient;

        public GetLastMerchantTransactionIntent(string accessToken)
        {
            _monzoClient = new MonzoClient(accessToken);
        }

        public string IntentName => "GetLastMerchantTransactionIntent";
        public string Execute(Intent context, MonzoResource resource)
        {
            var merchant = context.Slots["Merchant"].Value;

            var account = _monzoClient.GetAccounts().Result;

            var validAccount = account.First(x => !x.Closed);

            var transactions = _monzoClient.GetTransactions(validAccount).Result;

            var merchantTransactions =
                transactions.Where(x => x.Description.Contains(merchant, StringComparison.OrdinalIgnoreCase));

            var lastTransaction = merchantTransactions.OrderByDescending(x => x.Created).FirstOrDefault();

            if (lastTransaction == null)
            {
                return $"I'm sorry, I couldn't find any transactions for {merchant}";
            }

            var day = lastTransaction.Created.Day.ToOrdinalWords();
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(lastTransaction.Created.Month);
            var year = lastTransaction.Created.Year;
            var amount = CurrencyHelper.GetAmountString(Math.Abs(lastTransaction.Amount));

            return $"Your last transaction at {merchant} was on the {day} of {month} {year} for {amount.Amount}";
        }

        public bool ShouldEndSession => false;
    }
}
