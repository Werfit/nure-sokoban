using System;
using Sokoban.UI.Game;

using Sokoban.History;

namespace Sokoban.Commands
{
    public class UndoCommand : Command
    {
        private SokobanGame _application;

        public UndoCommand(SokobanGame application)
        {
            _application = application;
        }

        public override void Execute()
        {
            if (_application.currentViewHistory.Count == 0)
            {
                return;
            }
            List<Snapshot> history = _application.currentViewHistory;

            Snapshot snapshot = history.Last();
            history.RemoveAt(history.Count - 1);

            snapshot.Restore();
        }
    }
}

