using System;
using Alexa.NET.Request;
using Alexa.NET.Response;
using MonzoAlexa.Monzo;

namespace MonzoAlexa.Intents.IntentTypes
{
    public class GetBalanceIntent : IIntent
    {
        public string IntentName => "GetBalanceIntent";

        public IOutputSpeech Execute(Intent context, MonzoResource resource)
        {
            var firstNumber = Convert.ToInt32(context.Slots["FirstNumber"].Value);
            var secondNumber = Convert.ToInt32(context.Slots["SecondNumber"].Value);

            return new PlainTextOutputSpeech
            {
                Text = $"Number1: {firstNumber}. Number2: {secondNumber}"
            };
        }

        public bool ShouldEndSession => false;
    }
}
