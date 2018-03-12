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

        public IntentFactory()
        {
            _intents = new List<IIntent>
            {
                //Base intents
                new HelpIntent(),
                new CancelIntent(),
                new StopIntent(),

                //Custom intents
                new GetBalanceIntent()
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
