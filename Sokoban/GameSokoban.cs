
namespace Sokoban;

using UI.Game;
using UI.Components;
using Commands;
using History;

public class SokobanGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GameView currentView;

    private KeyboardState _previousKeyboardState;
    private MouseState _previousMouseState;

    private Dashboard _dashboard;
    public readonly List<Snapshot> currentViewHistory = new List<Snapshot>();

    public SokobanGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        currentView = new GameView(GraphicsDevice, Constants.LevelPaths[0], _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _dashboard = new Dashboard(GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, this);


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        currentView.Update(gameTime);

        if (currentView.toSave)
        {
            currentViewHistory.Add(currentView.SaveState());
        }

        _dashboard.Update();

        _previousKeyboardState = Keyboard.GetState();
        //_previousMouseState = mouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        currentView.Draw(_spriteBatch);
        _dashboard.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

