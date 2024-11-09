using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Game1 : Game
{
    public const int SCREEN_HEIGHT = 960;
    public const int SCREEN_WIDTH = 1280;
    public static bool ExitGame = false;
    // used so that every file has access to the content manager
    public static ContentManager ContentManager;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 960;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ContentManager = Content;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ButtonBar.Texture = ContentManager.Load<Texture2D>("ButtonBar");
        Button.Texture = ContentManager.Load<Texture2D>("Button");
        Button.ButtonFont = ContentManager.Load<SpriteFont>("ButtonFont");

        GamestateManager.AddGamestate(new MainMenu());
    }

    protected override void Update(GameTime gameTime)
    { 
        GamestateManager.CurrentGamestate().Update(gameTime);
        if (ExitGame) this.Exit();

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
