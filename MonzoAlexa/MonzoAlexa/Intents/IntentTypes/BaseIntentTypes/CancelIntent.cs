using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class CancelIntent : IIntent
    {
        public string IntentName => "AMAZON.CancelIntent";
        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.StopMessage;
        }

        public bool ShouldEndSession => true;
    }
}
