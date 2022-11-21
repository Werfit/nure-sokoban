using Sokoban.Utils;

namespace Sokoban.UI.Game
{
    public class Box : Sprite
    {
        public bool onSite
        {
            get
            {
                return _onSite;
            }
        }

        private bool _onSite = false;
        private Rectangle _previousRectangle;

        public Rectangle previousRectangle
        {
            get
            {
                return _previousRectangle;
            }
        }

        public Box(GraphicsDevice graphicsDevice, int x, int y, bool isOnSite = false) : base(graphicsDevice, x, y, Constants.AssetsPaths.BoxInactive, Color.Brown)
        {
            if (isOnSite)
            {
                ChangeSiteState(isOnSite);
            }

            _previousRectangle = destinationRectangle;
        }

        public void Update(List<Site> sites)
        {
            foreach (Site site in sites)
            {
                if (site.destinationRectangle.X == _x && site.destinationRectangle.Y == _y)
                {
                    ChangeSiteState(true);
                    return;
                }
            }

            ChangeSiteState(false);
        }

        public Vector2 GetNextPosition(Direction horizontal, Direction vertical)
        {
            int y = _y + (int)vertical * _height;
            int x = _x + (int)horizontal * _width;

            return new Vector2(x, y);
        }

        public void Move(Direction horizontal, Direction vertical)
        {
            Vector2 position = GetNextPosition(horizontal, vertical);

            _x = (int)position.X;
            _y = (int)position.Y;
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        private void ChangeSiteState(bool value)
        {
            if (value == _onSite)
            {
                return;
            }
            _onSite = value;

            string path = _onSite ? Constants.AssetsPaths.BoxActive : Constants.AssetsPaths.BoxInactive;

            FileStream fs = new FileStream(Assets.GetAbsolutePath(path), FileMode.Open);
            _texture = Texture2D.FromStream(_graphicsDevice, fs);
        }

        public void UpdatePreviousPosition()
        {
            _previousRectangle = destinationRectangle;
        }
    }
}

