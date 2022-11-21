using Sokoban.Utils;
using Sokoban.Levels;
using Sokoban.History;

namespace Sokoban.UI.Game
{
    public class GameView
    {
        private string _mapPath;
        private GraphicsDevice _graphicsDevice;

        private readonly List<Wall> _walls = new List<Wall>();
        private readonly List<Box> _boxes = new List<Box>();
        private readonly List<Site> _sites = new List<Site>();
        private readonly List<Space> _spaces = new List<Space>();

        private Player _player;
        private bool _toSave = false;

        private readonly int _screenWidth, _screenHeight;

        private bool gameOver = false;

        public bool toSave
        {
            get
            {
                return _toSave;
            }
        }

        public GameView(GraphicsDevice graphicsDevice, string mapPath, int screenWidth, int screenHeight)
        {
            _graphicsDevice = graphicsDevice;
            _mapPath = mapPath;

            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            LoadContent();
        }

        public void LoadContent()
        {
            Level lvl = new Level(Assets.GetAbsolutePath(_mapPath));
            Element[] elements = lvl.ReadMap();

            foreach (Element element in elements)
            {
                if (element.type == ElementType.Wall)
                {
                    Wall tmp = new Wall(_graphicsDevice, element.x, element.y);
                    _walls.Add(tmp);
                }

                if (element.type == ElementType.Box)
                {
                    Box tmp = new Box(_graphicsDevice, element.x, element.y);
                    _boxes.Add(tmp);
                }

                if (element.type == ElementType.Site)
                {
                    Site tmp = new Site(_graphicsDevice, element.x, element.y);
                    _sites.Add(tmp);
                }

                if (element.type == ElementType.BoxOnSite)
                {
                    Site tmpSite = new Site(_graphicsDevice, element.x, element.y);
                    Box tmpBox = new Box(_graphicsDevice, element.x, element.y, true);

                    _sites.Add(tmpSite);
                    _boxes.Add(tmpBox);

                }

                if (element.type == ElementType.Player)
                {
                    _player = new Player(_graphicsDevice, element.x, element.y);
                }

            }

            for (int y = 0; y < _screenHeight; y += 50)
            {
                for (int x = 0; x < _screenWidth; x += 50)
                {

                    Space tmp = new Space(_graphicsDevice, x, y);
                    _spaces.Add(tmp);
                }
            }
        }

        // Update logic
        public void Update(GameTime gameTime)
        {
            _toSave = false;

            if (gameOver)
            {
                return;
            }

            _player?.Update(_boxes, _walls);

            bool isOver = true;
            foreach (Box box in _boxes)
            {
                box.Update(_sites);
                if (!box.onSite)
                {
                    isOver = false;
                }
            }

            _toSave = _player.hasPositionChanged;
            gameOver = isOver;
        }

        // Draw logic
        public void Draw(SpriteBatch spriteBatch)
        {
            if (gameOver)
            {
                return;
            }

            foreach (Space space in _spaces)
            {
                spriteBatch.Draw(space, space.destinationRectangle, space.sourceRectangle, space.color);
            }

            foreach (Wall wall in _walls)
            {
                spriteBatch.Draw(wall, wall.destinationRectangle, wall.sourceRectangle, wall.color);
            }


            foreach (Site site in _sites)
            {
                spriteBatch.Draw(site, site.destinationRectangle, site.sourceRectangle, site.color);
            }

            foreach (Box box in _boxes)
            {
                spriteBatch.Draw(box, box.destinationRectangle, box.sourceRectangle, box.color);
            }

            if (_player is not null)
            {
                spriteBatch.Draw(_player, _player.destinationRectangle, _player.sourceRectangle, _player.color);
            }
        }

        public Snapshot SaveState()
        {
            Rectangle playerRectangle = _player.previousRectangle;

            List<Rectangle> boxesRectangles = new List<Rectangle>();
            foreach (Box box in _boxes) boxesRectangles.Add(box.previousRectangle);

            return new Snapshot(this, playerRectangle, boxesRectangles);
        }

        public void SetPlayerPosition(Rectangle rect)
        {
            _player.SetPosition(rect.X, rect.Y);
        }

        public void SetBoxesPosition(List<Rectangle> rects)
        {
            for (int i = 0; i < _boxes.Count; i++) _boxes[i].SetPosition(rects[i].X, rects[i].Y);
        }
    }
}

