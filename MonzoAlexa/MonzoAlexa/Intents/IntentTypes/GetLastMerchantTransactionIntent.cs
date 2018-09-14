using System;
using System.Globalization;
using System.Linq;
using Alexa.NET.Request;
using Amazon.Lambda.Core;
using Humanizer;
using MonzoAlexa.ExtensionMethods;
using MonzoAlexa.Helpers;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetLastMerchantTransactionIntent : IIntent
    {
        private readonly ILambdaLogger _logger;
        private readonly IMonzoClient _monzoClient;

        public GetLastMerchantTransactionIntent(string accessToken, ILambdaLogger logger)
        {
            _logger = logger;
            _monzoClient = new MonzoClient(accessToken, _logger);
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

            var amount = CurrencyHelper.GetAmountString(Math.Abs(lastTransaction.Amount));

            var isYesterday = DateTime.Now.Date.AddDays(-1) == lastTransaction.Created.Date;
            var isToday = DateTime.Now.Date.AddDays(0) == lastTransaction.Created.Date;

            string dateMessage;

            if (isYesterday)
            {
                dateMessage = "yesterday";
            }
            else if (isToday)
            {
                dateMessage = "today";
            }
            else
            {
                var day = lastTransaction.Created.Day.ToOrdinalWords();
                var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(lastTransaction.Created.Month);
                var year = lastTransaction.Created.Year;

                dateMessage = $"on the {day} of {month} {year}";
            }

            return $"Your last transaction at {merchant} was {dateMessage} for {amount.Amount}";
        }

        public bool ShouldEndSession => true;
    }
}
