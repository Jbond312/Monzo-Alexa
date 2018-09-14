using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using MonzoAlexa.Intents.IntentTypes;
using MonzoAlexa.Intents.IntentTypes.BaseIntentTypes;

namespace MonzoAlexa.Intents
{
    public class IntentFactory
    {
        private readonly ILambdaLogger _logger;
        private readonly List<IIntent> _intents;
        private readonly IIntent _unknownIntent;

        public IntentFactory(string accessToken, ILambdaLogger logger)
        {
            _logger = logger;
            _intents = new List<IIntent>
            {
                //Base intents
                new HelpIntent(),
                new CancelIntent(),
                new StopIntent(),
                new UnknownIntent(),

                //Custom intents
                new GoAwayIntent(),
                new GetBalanceIntent(accessToken, logger),
                new GetAccountIntent(accessToken, logger),
                new GetLastMerchantTransactionIntent(accessToken, logger),
                new GetSpendingToday(accessToken, _logger)
            };

            _unknownIntent = new UnknownIntent();
        }

        public IIntent GetIntent(string intentName)
        {
            var foundIntent = _intents.FirstOrDefault(x => x.IntentName.Equals(intentName, StringComparison.OrdinalIgnoreCase));

            return foundIntent ?? _unknownIntent;
        }
    }
}
