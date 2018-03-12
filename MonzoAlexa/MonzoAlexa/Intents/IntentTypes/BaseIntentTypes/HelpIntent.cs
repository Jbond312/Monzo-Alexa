using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class HelpIntent : IIntent
    {
        public string IntentName => "AMAZON.HelpIntent";

        public IOutputSpeech Execute(Intent context, MonzoResource resource)
        {
            return new PlainTextOutputSpeech
            {
                Text = resource.HelpMessage
            };
        }

        public bool ShouldEndSession => false;
    }
}
