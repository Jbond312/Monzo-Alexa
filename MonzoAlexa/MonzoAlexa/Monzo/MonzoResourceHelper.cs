using System.Collections.Generic;
using System.Linq;

namespace MonzoAlexa.Monzo
{
    public static class MonzoResourceHelper
    {
        public static MonzoResource GetResources(string locale = "en-GB")
        {
            var resources = new List<MonzoResource>();
            var enUsResource = new MonzoResource("en-US")
            {
                SkillName = "Monzo Alexa",
                HelpMessage = "You can say What is my balance, or, How much have I spent today... What can I help you with?",
                HelpReprompt = "What can I help you with?",
                StopMessage = "Goodbye!"
            };
            var enGbResource = new MonzoResource("en-GB")
            {
                SkillName = "Monzo Alexa",
                HelpMessage = "You can say What is my balance, or, How much have I spent today... What can I help you with?",
                HelpReprompt = "What can I help you with?",
                StopMessage = "Goodbye!"
            };
            var deDeResource = new MonzoResource("de-DE")
            {
                SkillName = "Monzo Alexa",
                HelpMessage = "Du kannst Wie hoch ist mein Guthaben?, oder, Wie viel habe ich heute ausgegeben sagen.... Wie kann ich Ihnen Helfen??",
                HelpReprompt = "Wie kann ich Ihnen Helfen?",
                StopMessage = "Tschüss!"
            };
            resources.Add(enUsResource);
            resources.Add(enGbResource);
            resources.Add(deDeResource);

            var matchedLocale = resources.FirstOrDefault(x => x.Language.Equals(locale));

            if (locale == null)
            {
                matchedLocale = resources.First(x => x.Language == "en-GB");
            }

            return matchedLocale;
        }
    }
}
