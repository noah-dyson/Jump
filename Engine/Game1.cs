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
    public static MouseState PreviousMouse;
    public static MouseState CurrentMouse;
    public static GameWindow CurrentWindow;

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

        CurrentWindow = Window;

        ButtonBar.Texture = ContentManager.Load<Texture2D>("PlainSquare");
        Button.Texture = ContentManager.Load<Texture2D>("PlainSquare");
        Button.ObjectTilemap = ContentManager.Load<Texture2D>("Tilemap");
        Button.ButtonFont = ContentManager.Load<SpriteFont>("ButtonFont");
        Highlighter.Texture = ContentManager.Load<Texture2D>("Highlighter");
        CellLabel.CellLabelFont = ContentManager.Load<SpriteFont>("ButtonFont");
        LevelObject.TileMap = ContentManager.Load<Texture2D>("Tilemap");
        EditorObject.TileMap = ContentManager.Load<Texture2D>("Tilemap");
        PlayerCharacter.Texture = ContentManager.Load<Texture2D>("PlayerCharacter");
        Timer.Font = ContentManager.Load<SpriteFont>("ButtonFont");
        TextBox.Background = ContentManager.Load<Texture2D>("PlainSquare");
        BrowserItem.Font = ContentManager.Load<SpriteFont>("ButtonFont");
        KeyIcon.Texture = ContentManager.Load<Texture2D>("KeyIcon");

        GamestateManager.AddGamestate(new MainMenu());
    }

    protected override void Update(GameTime gameTime) {
        PreviousMouse = CurrentMouse;
        CurrentMouse = Mouse.GetState();
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
