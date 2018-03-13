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
        private readonly List<IIntent> _intents;
        private readonly IIntent _unknownIntent;

        public IntentFactory(string accessToken, ILambdaLogger log)
        {
            _intents = new List<IIntent>
            {
                //Base intents
                new HelpIntent(log),
                new CancelIntent(log),
                new StopIntent(log),

                //Custom intents
                new GetBalanceIntent(accessToken, log)
            };

            _unknownIntent = new UnknownIntent(log);
        }

        public IIntent GetIntent(string intentName)
        {
            var foundIntent = _intents.FirstOrDefault(x => x.IntentName.Equals(intentName, StringComparison.OrdinalIgnoreCase));

            return foundIntent ?? _unknownIntent;
        }
    }
}
