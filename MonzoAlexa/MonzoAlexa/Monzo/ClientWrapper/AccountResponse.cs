using System.Collections.Generic;
using MonzoAlexa.Monzo.ClientWrapper.Entities;

namespace MonzoAlexa.Monzo.ClientWrapper
{
    public class AccountResponse
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}
