using Sokoban.Commands;

namespace Sokoban.UI.Components
{
    public class Dashboard
    {
        private readonly GraphicsDevice _graphicsDevice;

        private readonly Button _restartButton;
        private readonly Button _firstLevelButton;
        private readonly Button _secondLevelButton;
        private readonly Button _undoButton;

        private int _currentLevelIndex = 0;
        private MouseState _previousState;

        public Dashboard(GraphicsDevice graphicsDevice, int screenWidth, int screenHeight, SokobanGame application)
        {
            _graphicsDevice = graphicsDevice;

            Vector2 windowSize = new Vector2(screenWidth, screenHeight);

            RestartGameCommand restartGameCommand = new(Constants.LevelPaths[_currentLevelIndex], windowSize, application);
            RestartGameCommand firstLevelGameCommand = new(Constants.LevelPaths[0], windowSize, application);
            RestartGameCommand secondLevelGameCommand = new(Constants.LevelPaths[1], windowSize, application);

            UndoCommand undoCommand = new(application);


            _restartButton = new Button(_graphicsDevice, Color.DarkBlue, GenerateRectangle(screenWidth - 100, 50), new List<Commands.Command>() { restartGameCommand });
            _firstLevelButton = new Button(_graphicsDevice, Color.DarkGoldenrod, GenerateRectangle(screenWidth - 100, 150), new List<Commands.Command>()
            {
                firstLevelGameCommand
            });
            _secondLevelButton = new Button(_graphicsDevice, Color.DarkKhaki, GenerateRectangle(screenWidth - 100, 200), new List<Commands.Command>()
            {
                secondLevelGameCommand
            });

            _undoButton = new Button(_graphicsDevice, Color.DarkTurquoise, GenerateRectangle(screenWidth - 100, 300), new List<Commands.Command>()
            {
                undoCommand
            });

            Rectangle GenerateRectangle(int x, int y)
            {
                return new Rectangle(x, y, Constants.BlockSize, Constants.BlockSize);
            }
        }

        public void Update()
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton != ButtonState.Released || _previousState.LeftButton != ButtonState.Pressed)
            {
                _previousState = state;
                return;
            }

            if (_firstLevelButton.Contains(state.X, state.Y))
            {
                _firstLevelButton.OnClick();
            }

            if (_secondLevelButton.Contains(state.X, state.Y))
            {
                _secondLevelButton.OnClick();
            }

            if (_restartButton.Contains(state.X, state.Y))
            {
                _restartButton.OnClick();
            }

            if (_undoButton.Contains(state.X, state.Y))
            {
                _undoButton.OnClick();
            }

            _previousState = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_firstLevelButton, _firstLevelButton.rectangle, _firstLevelButton.backgroundColor);
            spriteBatch.Draw(_secondLevelButton, _secondLevelButton.rectangle, _secondLevelButton.backgroundColor);
            spriteBatch.Draw(_restartButton, _restartButton.rectangle, _restartButton.backgroundColor);
            spriteBatch.Draw(_undoButton, _undoButton.rectangle, _undoButton.backgroundColor);
        }
    }
}

