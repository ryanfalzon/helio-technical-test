using System.Transactions;

namespace PhoneBook.Lib.App;

public class TransactionScopeFactory
{
    public static TransactionScope CreateReadCommittedAsync() => new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
    {
        IsolationLevel = IsolationLevel.ReadCommitted
    }, TransactionScopeAsyncFlowOption.Enabled);
}