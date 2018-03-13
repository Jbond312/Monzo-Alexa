using System;
using System.Collections.Generic;
using System.Linq;
using MonzoAlexa.Intents.IntentTypes;
using MonzoAlexa.Intents.IntentTypes.BaseIntentTypes;

namespace MonzoAlexa.Intents
{
    public class IntentFactory
    {
        private readonly List<IIntent> _intents;
        private readonly IIntent _unknownIntent;

        public IntentFactory(string accessToken)
        {
            _intents = new List<IIntent>
            {
                //Base intents
                new HelpIntent(),
                new CancelIntent(),
                new StopIntent(),
                new UnknownIntent(),

                //Custom intents
                new GetBalanceIntent(accessToken),
                new GetAccountIntent(accessToken),
                new GetLastMerchantTransactionIntent(accessToken)
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
