using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class CancelIntent : IIntent
    {
        public CancelIntent(ILambdaLogger log)
        {
        }

        public string IntentName => "AMAZON.CancelIntent";
        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.StopMessage;
        }

        public bool ShouldEndSession => true;
    }
}
