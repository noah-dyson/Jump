using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class MainMenu : Gamestate {
    private ButtonBar _mainMenuBar;
    private Button _levelEditorButton;
    private Button _levelBrowserButton;
    private Button _gameplayButton;
    private Button _quitButton;
    private SpriteFont _titleFont;
    private Vector2 _position;

    public override void LoadContent() {
        // loads title font and measure the length of the title for positioning
        _titleFont = Game1.ContentManager.Load<SpriteFont>("TitleFont");
        int length = (int)_titleFont.MeasureString("JUMP").X;
        _position = new Vector2(Game1.SCREEN_WIDTH / 2 - length / 2, 200);

        int pad = 50;
        int height = 280;
        int width = Game1.SCREEN_WIDTH - 2 * pad;

        _mainMenuBar = new ButtonBar(new Vector2(pad, Game1.SCREEN_HEIGHT - pad - height), width, height);
        _levelEditorButton = new Button(320, 220, "Level Editor");
        _levelBrowserButton = new Button(320, 220, "Level Browser");
        _gameplayButton = new Button(320, 220, "Gameplay");
        _quitButton = new Button(320, 220, "Quit");

        // subscribes the relevant click method to the relevant event
        _levelEditorButton.Clicked += LevelEditorButton_Clicked;
        _levelBrowserButton.Clicked += LevelBrowserButton_Clicked;
        _gameplayButton.Clicked += GameplayButton_Clicked;
        _quitButton.Clicked += QuitButton_Clicked;

        _mainMenuBar.Buttons = new List<Button>() { _levelEditorButton, _levelBrowserButton, _gameplayButton, _quitButton };
        // distributes the buttons within the button bar
        _mainMenuBar.SetButtonPositions();

        AddObject(_mainMenuBar);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.DrawString(_titleFont, "JUMP", _position, Color.White);
        base.Draw(spriteBatch);
    }

    private void LevelEditorButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new LevelEditor());
    }

    private void LevelBrowserButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new LevelBrowser());
    }

    private void GameplayButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new LevelGameplay());
    }

    private void QuitButton_Clicked(object sender, EventArgs e) {
        Game1.ExitGame = true;
    }

}
