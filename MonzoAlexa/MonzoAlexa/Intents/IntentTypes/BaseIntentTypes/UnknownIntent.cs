using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class UnknownIntent : IIntent
    {
        public string IntentName => "UnknownIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return "I'm sorry, I'm afraid I don't understand.";
        }

        public bool ShouldEndSession => false;
    }
}
