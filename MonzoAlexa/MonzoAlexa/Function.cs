using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using MonzoAlexa.Intents;
using MonzoAlexa.Monzo;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MonzoAlexa
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var response = new SkillResponse
            {
                Response = new ResponseBody
                {
                    ShouldEndSession = false
                }
            };

            IOutputSpeech innerResponse = null;

            var log = context.Logger;
            log.LogLine("Skill Request Object:");
            log.LogLine(JsonConvert.SerializeObject(input));

            var resource = MonzoResourceHelper.GetResources(input.Request.Locale);

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                log.LogLine("Default LaunchRequest made: 'Alexa, open Monzo");
                innerResponse = new PlainTextOutputSpeech
                {
                    Text = resource.HelpMessage
                };
                response.Response.ShouldEndSession = false;
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest) input.Request;

                var inputJson = JsonConvert.SerializeObject(input);

                log.LogLine($"Logging Input: {inputJson}");

                var accessToken = input.Session.User.AccessToken;
                var intentFactory = new IntentFactory(accessToken);

                log.LogLine($"{intentRequest.Intent.Name}");

                var activatedIntent = intentFactory.GetIntent(intentRequest.Intent.Name);
                
                var message = activatedIntent.Execute(intentRequest.Intent, resource);

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = message;
                response.Response.ShouldEndSession = activatedIntent.ShouldEndSession;
            }

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            log.LogLine("Skill Response Object...");
            log.LogLine(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
