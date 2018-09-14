using System;
using System.Linq;
using Alexa.NET.Request;
using Amazon.Lambda.Core;
using MonzoAlexa.Helpers;
using MonzoAlexa.Monzo;
using MonzoAlexa.Monzo.ClientWrapper;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetSpendingToday : IIntent
    {
        private readonly ILambdaLogger _logger;
        private readonly IMonzoClient _monzoClient;

        public GetSpendingToday(string accessToken, ILambdaLogger logger)
        {
            _logger = logger;
            _monzoClient = new MonzoClient(accessToken, _logger);
        }

        public string IntentName => "GetSpendingTodayIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            var account = _monzoClient.GetAccounts().Result;

            var validAccount = account.First(x => !x.Closed);

            var today = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc);

            var transactions = _monzoClient.GetTransactions(validAccount, today).Result;

            var totalAmount = transactions?.Sum(x => x.Amount) ?? 0;

            var currencyData = CurrencyHelper.GetAmountString(Math.Abs(totalAmount));

            return totalAmount == 0 ? "You haven't spent anything today" : $"Today you've spent a total of {currencyData.Amount}";
        }

        public bool ShouldEndSession => true;
    }
}
