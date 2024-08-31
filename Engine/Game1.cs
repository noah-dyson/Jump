using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Game1 : Game
{
    public const int SCREEN_HEIGHT = 480;
    public const int SCREEN_WIDTH = 640;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 480;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GamestateManager.AddGamestate(new MainMenu());

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ButtonBar.Texture = Content.Load<Texture2D>("ButtonBar");
        Button.Texture = Content.Load<Texture2D>("Button");
        Button.ButtonFont = Content.Load<SpriteFont>("ButtonFont");

        GamestateManager.CurrentGamestate().LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    { 
        GamestateManager.CurrentGamestate().Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(34, 32, 52));

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        GamestateManager.CurrentGamestate().Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
