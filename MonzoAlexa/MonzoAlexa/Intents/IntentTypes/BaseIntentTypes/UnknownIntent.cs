using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class UnknownIntent : IIntent
    {
        public string IntentName => "Unknown";
        public IOutputSpeech Execute(Intent context, MonzoResource resource)
        {
            return new PlainTextOutputSpeech
            {
                Text = resource.HelpReprompt
            };
        }

        public bool ShouldEndSession => false;
    }
}
