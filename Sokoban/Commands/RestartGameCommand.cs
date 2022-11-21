using Sokoban.UI.Game;

namespace Sokoban.Commands
{
    public class RestartGameCommand : Command
    {
        private string _path;
        private Vector2 _windowSize;
        private SokobanGame _application;

        public RestartGameCommand(string path, Vector2 size, SokobanGame application)
        {
            _path = path;
            _windowSize = size;
            _application = application;
        }

        public override void Execute()
        {
            _application.currentView = new GameView(_application.GraphicsDevice, _path, (int)_windowSize.X, (int)_windowSize.Y);
        }
    }
}

