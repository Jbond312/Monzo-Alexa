using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class UnknownIntent : IIntent
    {
        public UnknownIntent(ILambdaLogger log)
        {
        }

        public string IntentName => "Unknown";
        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.HelpReprompt;
        }

        public bool ShouldEndSession => false;
    }
}
