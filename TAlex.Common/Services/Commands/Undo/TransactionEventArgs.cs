using System;

namespace TAlex.Common.Services.Commands.Undo
{
    public enum TransactionEventArgs
    {
        Commit,
        CommitUndo,
        CommitRedo,
        Rollback
    }
}
