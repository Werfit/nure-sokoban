using System;
using Microsoft.Xna.Framework.Graphics;
using Sokoban.Utils;

namespace Sokoban.UI.Game
{
    public abstract class Sprite
    {
        protected GraphicsDevice _graphicsDevice;
        protected int _x, _y, _width, _height;
        protected Color _color;
        protected Texture2D _texture;

        public readonly Color color = Color.White;
        public readonly Rectangle sourceRectangle;

        public Rectangle destinationRectangle
        {
            get
            {
                return new Rectangle(_x, _y, _width, _height);
            }
        }

        public Sprite(GraphicsDevice graphicsDevice, int x, int y, string path, Color color)
        {
            _x = x;
            _y = y;
            _width = Constants.BlockSize;
            _height = Constants.BlockSize;

            _graphicsDevice = graphicsDevice;

            FileStream fs = new FileStream(Assets.GetAbsolutePath(path), FileMode.Open);
            _texture = Texture2D.FromStream(_graphicsDevice, fs);
            sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public void UnloadContent()
        {
            _texture.Dispose();
        }

        public static implicit operator Texture2D(Sprite obj)
        {
            return obj._texture;
        }
    }
}

