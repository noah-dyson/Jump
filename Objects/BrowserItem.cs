using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class BrowserItem : GameObject {
    public static SpriteFont Font;
    private ButtonBar _backBar, _optionBar;
    private Button _editButton, _playButton, _deleteButton, _renameButton;
    private Level _level;
    private string _levelName = "";
    private Vector2 _namePos;
    private Vector2 _timePos;
    private string _bestTimeText = "";
    private LevelBrowser _levelBrowser;

    public BrowserItem(Level level, LevelBrowser levelBrowser) {
        _level = level;
        _levelName = level.Name;
        _levelBrowser = levelBrowser;
    }

    public void InitialiseItem(Vector2 position, int width, int height) {
        // creates button bars for item
        _namePos = new Vector2(position.X + 30, position.Y + 5);
        _timePos = new Vector2(position.X + width / 2 + 30, position.Y + 5);
        _backBar = new ButtonBar(position, width, height);
        _optionBar = new ButtonBar(new Vector2(position.X, position.Y + height - 50), width, 50);

        // creates buttons and adds them to option bar
        _editButton = new Button(80, 30, "Edit");
        _playButton = new Button(80, 30, "Play");
        _deleteButton = new Button(80, 30, "Delete");
        _renameButton = new Button(80, 30, "Rename");
        _optionBar.Buttons = new List<Button>() { _playButton, _editButton, _deleteButton, _renameButton };
        _optionBar.SetButtonPositions();

        _playButton.Clicked += PlayButton_Clicked;
        _editButton.Clicked += EditButton_Clicked;
        _renameButton.Clicked += RenameButton_Clicked;
        _deleteButton.Clicked += DeleteButton_Clicked;

        _backBar.Buttons = new List<Button>() { };
    }

    private void PlayButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new LevelGameplay(_level));
    }

    private void EditButton_Clicked(object sender, EventArgs e) {
        GamestateManager.AddGamestate(new LevelEditor(_level));
    }

    private void DeleteButton_Clicked(object sender, EventArgs e) {
        File.Delete(_level.FilePath);
        // have to adjust previous mouse click to prevent multiple presses
        Game1.PreviousMouse = Mouse.GetState();
        _levelBrowser.ResetBrowser();
    }

    private void RenameButton_Clicked(object sender, EventArgs e) {
        // ensures browser is reset next frame
        _levelBrowser.PendingBrowserReset = true;
        GamestateManager.AddGamestate(new SaveAs(_level));
    }

    public override void Update(GameTime gameTime) {
        _optionBar.Update(gameTime);
        _backBar.Update(gameTime);

        // if level has not been played the time is zero
        if (_level.BestTime == TimeSpan.Zero) {
            _bestTimeText = "No Time Recorded";
        }
        else {
            _bestTimeText = _level.BestTime.ToString();
        }
    }

    public override void Draw(SpriteBatch spriteBatch) {
        _backBar.Draw(spriteBatch);
        _optionBar.Draw(spriteBatch);
        spriteBatch.DrawString(Font, _levelName, _namePos, Color.White);
        spriteBatch.DrawString(Font, _bestTimeText, _timePos, Color.White);
    }
}