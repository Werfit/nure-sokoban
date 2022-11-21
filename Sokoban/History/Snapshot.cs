using System;
using Sokoban.UI.Game;

namespace Sokoban.History
{
    public class Snapshot
    {
        private readonly Rectangle _playerRectangle;
        private readonly List<Rectangle> _boxesRectangles;
        private readonly GameView _view;

        public Snapshot(GameView view, Rectangle playerRectangle, List<Rectangle> boxesRectangles)
        {
            _playerRectangle = playerRectangle;
            _boxesRectangles = boxesRectangles;
            _view = view;
        }

        public void Restore()
        {
            _view.SetPlayerPosition(_playerRectangle);
            _view.SetBoxesPosition(_boxesRectangles);
        }
    }
}

