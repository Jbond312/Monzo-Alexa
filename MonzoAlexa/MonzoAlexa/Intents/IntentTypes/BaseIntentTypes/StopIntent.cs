using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class StopIntent : IIntent
    {
        public string IntentName => "AMAZON.StopIntent";

        public IOutputSpeech Execute(Intent context, MonzoResource resource)
        {
            return new PlainTextOutputSpeech
            {
                Text = resource.StopMessage
            };
        }

        public bool ShouldEndSession => true;
    }
}
