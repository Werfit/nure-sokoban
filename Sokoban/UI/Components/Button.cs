using Sokoban.Commands;
using Sokoban.UI.Game;

namespace Sokoban.UI.Components
{
    public class Button
    {
        protected readonly Texture2D _texture;
        protected readonly GraphicsDevice _graphicsDevice;

        public readonly Rectangle rectangle;
        public readonly Color backgroundColor;

        protected readonly List<Command> _commands;

        protected readonly string _content;

        public Button(GraphicsDevice graphicsDevice, Color backgroundColor, Rectangle rectangle, List<Command> commands)
        {
            _graphicsDevice = graphicsDevice;
            this.backgroundColor = backgroundColor;
            this.rectangle = rectangle;

            _texture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);
            SetColor();

            _commands = commands;
        }

        protected void SetColor()
        {
            Color[] colors = new Color[rectangle.Width * rectangle.Height];
            for (int i = 0; i < colors.Length; i++) colors[i] = backgroundColor;
            _texture.SetData(colors);
        }

        public void UnloadContent()
        {
            _texture.Dispose();
        }

        public void OnClick()
        {
            foreach (Command command in _commands) command.Execute();
        }

        public bool Contains(int x, int y)
        {
            return rectangle.Contains(x, y);
        }

        public static implicit operator Texture2D(Button obj)
        {
            return obj._texture;
        }
    }
}

