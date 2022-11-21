using System;
namespace Sokoban.UI.Game
{
    public class Site : Sprite
    {
        public Site(GraphicsDevice graphicsDevice, int x, int y) : base(graphicsDevice, x, y, Constants.AssetsPaths.Site, Color.Chartreuse)
        {
        }
    }
}

