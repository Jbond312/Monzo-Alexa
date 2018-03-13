using System.Collections.Generic;
using System.Threading.Tasks;
using MonzoAlexa.Monzo.ClientWrapper.Entities;

namespace MonzoAlexa.Monzo.ClientWrapper
{
    public interface IMonzoClient
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<int> GetBalance(Account account);
        Task<IEnumerable<Transaction>> GetTransactions(Account account);
    }
}
