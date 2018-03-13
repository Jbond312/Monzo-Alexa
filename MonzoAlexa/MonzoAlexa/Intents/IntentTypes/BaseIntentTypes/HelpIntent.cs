using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class HelpIntent : IIntent
    {
        public string IntentName => "AMAZON.HelpIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.HelpMessage;
        }

        public bool ShouldEndSession => false;
    }
}
