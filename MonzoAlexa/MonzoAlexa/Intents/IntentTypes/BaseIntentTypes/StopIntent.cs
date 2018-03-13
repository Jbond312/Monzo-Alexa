using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class StopIntent : IIntent
    {
        public StopIntent(ILambdaLogger log)
        {
        }

        public string IntentName => "AMAZON.StopIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.StopMessage;
        }

        public bool ShouldEndSession => true;
    }
}
