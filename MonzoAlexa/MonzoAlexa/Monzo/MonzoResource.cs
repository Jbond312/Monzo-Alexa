namespace MonzoAlexa.Monzo
{
    public class MonzoResource
    {
        public MonzoResource(string language)
        {
            Language = language;
        }

        public string Language { get; set; }
        public string SkillName { get; set; }
        public string HelpMessage { get; set; }
        public string HelpReprompt { get; set; }
        public string StopMessage { get; set; }
    }
}
