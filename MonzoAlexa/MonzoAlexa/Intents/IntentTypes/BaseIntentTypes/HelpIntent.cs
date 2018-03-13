using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace MonzoAlexa.Intents.IntentTypes.BaseIntentTypes
{
    public class HelpIntent : IIntent
    {
        public HelpIntent(ILambdaLogger log)
        {
        }

        public string IntentName => "AMAZON.HelpIntent";

        public string Execute(Intent context, MonzoResource resource)
        {
            return resource.HelpMessage;
        }

        public bool ShouldEndSession => false;
    }
}
