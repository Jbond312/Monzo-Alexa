using System;

namespace MonzoAlexa.Monzo.ClientWrapper.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Closed { get; set; }
        public string Type { get; set; }
    }
}
