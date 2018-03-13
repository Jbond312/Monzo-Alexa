using Alexa.NET.Request;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents
{
    public interface IIntent
    {
        string IntentName { get; }
        string Execute(Intent context, MonzoResource resource);
        bool ShouldEndSession { get; }
    }
}
