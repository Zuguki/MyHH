using System.Transactions;

namespace FindWork.Test.Helpers;

public static class Helper
{
    public static TransactionScope CreateTransactionScope(int seconds = 1)
    {
        return new TransactionScope(TransactionScopeOption.Required, 
            new TimeSpan(0, 0, seconds), 
            TransactionScopeAsyncFlowOption.Enabled);
    }
}