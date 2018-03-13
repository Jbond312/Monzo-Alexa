using System.Collections.Generic;
using MonzoAlexa.Monzo.ClientWrapper.Entities;

namespace MonzoAlexa.Monzo.ClientWrapper
{
    public class TransactionResponse
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
