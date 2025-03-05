using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Button : GameObject {
    public static SpriteFont ButtonFont;
    public static Texture2D Texture;
    private Vector2 _position;
    private Vector2 _textPosition;
    public Vector2 Position {
        // adjusts button position to centre of button bar
        set { _position = new Vector2(value.X - (_width / 2), value.Y - (_height / 2)); }
    }
    public Vector2 TextPosition {
        // aligns text to the centre of the button
        set {
            _textPosition = new Vector2(value.X - ButtonFont.MeasureString(_text).X / 2,
                                          value.Y - ButtonFont.MeasureString(_text).Y / 2);
        }
    }
    private int _width;
    private int _height;
    private string _text;
    public int Id;
    private MouseState _previousMouse;
    private MouseState _currentMouse;
    private Rectangle _buttonBox {
        // produces a bounding box for detecting when mouse is over button 
        get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height); }
    }

    public Button(int width, int height, string text, int id = -1) {
        _width = width;
        _height = height;
        _text = text;
        Id = id;
    }

    public event EventHandler Clicked;

    public override void Update(GameTime gameTime) {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();

        // checks if the mouse is over the button
        // then checks if the left mouse button changes state, and thus is clicked
        if (_buttonBox.Contains(new Vector2(_currentMouse.X, _currentMouse.Y)) &&
            _previousMouse.LeftButton == ButtonState.Released &&
            _currentMouse.LeftButton == ButtonState.Pressed) {
            Clicked?.Invoke(this, new EventArgs());
        }
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, _buttonBox, new Color(59, 93, 201));
        spriteBatch.DrawString(ButtonFont, _text, _textPosition, Color.White);
    }
}
