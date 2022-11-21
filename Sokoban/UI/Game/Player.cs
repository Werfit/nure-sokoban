
using Sokoban.Utils;

namespace Sokoban.UI.Game
{
    public enum Direction
    {
        Increase = 1,
        Null = 0,
        Decrease = -1
    }

    public class Player : Sprite
    {
        private KeyboardState _previousKeyPressed;
        private KeyboardState _currentKeyPressed;
        private bool _changedPosition = false;

        private Rectangle _previousRectangle;

        public Rectangle previousRectangle
        {
            get
            {
                return _previousRectangle;
            }
        }

        public bool hasPositionChanged
        {
            get
            {
                return _changedPosition;
            }
        }

        public Player(GraphicsDevice graphicsDevice, int x, int y) : base(graphicsDevice, x, y, Constants.AssetsPaths.Player, Color.Beige)
        {
        }

        public void Update(List<Box> boxes, List<Wall> walls)
        {
            _previousKeyPressed = _currentKeyPressed;
            _currentKeyPressed = Keyboard.GetState();

            _previousRectangle = new Rectangle(_x, _y, _width, _height);

            int upcomingX = _x;
            int upcomingY = _y;

            Direction[] movementDirection = new Direction[2] { Direction.Null, Direction.Null };

            if (isKeyUp(Keys.S)) MoveVertically(Direction.Increase);
            if (isKeyUp(Keys.D)) MoveHorizontally(Direction.Increase);
            if (isKeyUp(Keys.W)) MoveVertically(Direction.Decrease);
            if (isKeyUp(Keys.A)) MoveHorizontally(Direction.Decrease);

            if (upcomingX == _x && upcomingY == _y)
            {
                _changedPosition = false;
                return;
            }

            Box playerNextBox = null;

            foreach (Box box in boxes)
            {
                if (box.destinationRectangle.X == upcomingX && box.destinationRectangle.Y == upcomingY)
                {
                    playerNextBox = box;
                }

                box.UpdatePreviousPosition();
            }

            if (playerNextBox is null)
            {
                Wall playerNextWall = walls.Find((Wall obj) => obj.destinationRectangle.X == upcomingX && obj.destinationRectangle.Y == upcomingY);

                if (playerNextWall is not null)
                {
                    _changedPosition = false;
                    return;
                }

                _x = upcomingX;
                _y = upcomingY;
                _changedPosition = true;

                return;
            }

            Vector2 boxNextPosition = playerNextBox.GetNextPosition(movementDirection[0], movementDirection[1]);

            Box boxNextBox = boxes.Find((Box obj) => obj.destinationRectangle.X == boxNextPosition.X && obj.destinationRectangle.Y == boxNextPosition.Y);

            if (boxNextBox is not null)
            {
                _changedPosition = false;
                return;
            }


            Wall boxNextWall = walls.Find((Wall obj) => obj.destinationRectangle.X == boxNextPosition.X && obj.destinationRectangle.Y == boxNextPosition.Y);

            if (boxNextWall is not null)
            {
                _changedPosition = false;
                return;
            }

            _x = upcomingX;
            _y = upcomingY;

            _changedPosition = true;
            playerNextBox.Move(movementDirection[0], movementDirection[1]);


            bool isKeyUp(Keys key)
            {
                return _previousKeyPressed.IsKeyDown(key) && _currentKeyPressed.IsKeyUp(key);
            }

            void MoveHorizontally(Direction direction)
            {
                movementDirection[0] = direction;
                upcomingX += (int)direction * _width;
            }

            void MoveVertically(Direction direction)
            {
                movementDirection[1] = direction;
                upcomingY += (int)direction * _height;
            }
        }

        public void SetPosition(int x, int y)
        {
            _changedPosition = false;

            _x = x;
            _y = y;
        }
    }
}

