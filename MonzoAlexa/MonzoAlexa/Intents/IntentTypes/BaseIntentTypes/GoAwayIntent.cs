using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class GoAwayIntent : IIntent
    {
        public string IntentName => "GoAwayIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return "Goodbye!";
        }

        public bool ShouldEndSession => true;
    }
}
