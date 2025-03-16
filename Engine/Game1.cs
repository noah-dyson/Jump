using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Game1 : Game {
    public const int SCREEN_HEIGHT = 900;
    public const int SCREEN_WIDTH = 1600;
    public static bool ExitGame = false;
    // used so that every file has access to the content manager
    public static ContentManager ContentManager;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        ContentManager = Content;
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        ButtonBar.Texture = ContentManager.Load<Texture2D>("PlainSquare");
        Button.Texture = ContentManager.Load<Texture2D>("PlainSquare");
        Button.ButtonFont = ContentManager.Load<SpriteFont>("ButtonFont");
        Highlighter.Texture = ContentManager.Load<Texture2D>("Highlighter");
        CellLabel.CellLabelFont = ContentManager.Load<SpriteFont>("ButtonFont");
        LevelObject.TileMap = ContentManager.Load<Texture2D>("Tilemap");
        EditorObject.TileMap = ContentManager.Load<Texture2D>("Tilemap");
        PlayerCharacter.Texture = ContentManager.Load<Texture2D>("PlayerCharacter");

        GamestateManager.AddGamestate(new MainMenu());
    }

    protected override void Update(GameTime gameTime) {
        GamestateManager.CurrentGamestate().Update(gameTime);
        if (ExitGame) this.Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(new Color(26, 28, 44));

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        GamestateManager.CurrentGamestate().Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
