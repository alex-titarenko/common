using System;

namespace TAlex.Common.Services.Commands.Undo
{
    public enum UndoState
    {
        Normal,
        Undo,
        Redo,
        Rollback
    }
}
