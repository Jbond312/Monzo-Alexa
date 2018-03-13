using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class StopIntent : IIntent
    {
        public string IntentName => "AMAZON.StopIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.StopMessage;
        }

        public bool ShouldEndSession => true;
    }
}
