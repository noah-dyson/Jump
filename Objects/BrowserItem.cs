using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class BrowserItem : GameObject {
    public static SpriteFont Font;
    private ButtonBar _backBar, _optionBar;
    private Button _editButton, _playButton, _deleteButton;
    private string _levelName = "";
    private Vector2 _namePos;

    public BrowserItem(string levelName) {
        _levelName = levelName;
    }

    public void InitialiseItem(Vector2 position, int width, int height) {
        _namePos = position;
        _backBar = new ButtonBar(position, width, height);
        _optionBar = new ButtonBar(new Vector2(position.X, position.Y + height - 50), width, 50);

        _editButton = new Button(60, 30, "Edit");
        _playButton = new Button(60, 30, "Play");
        _deleteButton = new Button(60, 30, "Delete");
        _optionBar.Buttons = new List<Button>() { _playButton, _editButton, _deleteButton };
        _optionBar.SetButtonPositions();
        _backBar.Buttons = new List<Button>() { };
    }

    public override void Update(GameTime gameTime) {
        _optionBar.Update(gameTime);
        _backBar.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        _backBar.Draw(spriteBatch);
        _optionBar.Draw(spriteBatch);
        spriteBatch.DrawString(Font, _levelName, _namePos, Color.White);
    }
}