using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents
{
    public interface IIntent
    {
        string IntentName { get; }
        IOutputSpeech Execute(Intent context, MonzoResource resource);
        bool ShouldEndSession { get; }
    }
}
