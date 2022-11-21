using System;

namespace Sokoban.UI.Game
{
    public class Wall : Sprite
    {
        public Wall(GraphicsDevice graphicsDevice, int x, int y) : base(graphicsDevice, x, y, Constants.AssetsPaths.Wall, Color.White)
        {
        }
    }
}

