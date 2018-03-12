using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class CancelIntent : IIntent
    {
        public string IntentName => "AMAZON.CancelIntent";
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
