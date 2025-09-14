using System.Transactions;

namespace TaskManagement.Api.Application.Helpers;

internal static class TransactionHelper
{
    public static TransactionScope GetTransactionScope()
    {
        return new(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(300) },
                    TransactionScopeAsyncFlowOption.Enabled);
    }
}
