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

            var logger = context.Logger;
            logger.LogLine("Skill Request Object:");
            logger.LogLine(JsonConvert.SerializeObject(input));

            var resource = MonzoResourceHelper.GetResources(input.Request.Locale);

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                logger.LogLine("Default LaunchRequest made: 'Alexa, open Monzo");
                innerResponse = new PlainTextOutputSpeech
                {
                    Text = resource.HelpMessage
                };
                response.Response.ShouldEndSession = false;
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var accessToken = input.Session.User.AccessToken;
                string message;
                if (!string.IsNullOrEmpty(accessToken))
                {

                    var intentRequest = (IntentRequest) input.Request;

                    var inputJson = JsonConvert.SerializeObject(input);

                    logger.LogLine($"Logging Input: {inputJson}");


                    var intentFactory = new IntentFactory(accessToken, logger);

                    logger.LogLine($"{intentRequest.Intent.Name}");

                    var activatedIntent = intentFactory.GetIntent(intentRequest.Intent.Name);

                    message = activatedIntent.Execute(intentRequest.Intent, resource);
                    response.Response.ShouldEndSession = activatedIntent.ShouldEndSession;
                }
                else
                {
                    message = "I'm sorry, I'm unable to communicate with the Monzo server. Please try again later.";
                    response.Response.ShouldEndSession = true;
                }

                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text = message;
                
            }

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            logger.LogLine("Skill Response Object...");
            logger.LogLine(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
