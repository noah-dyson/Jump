using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Button : GameObject {
    public static SpriteFont ButtonFont;
    public static Texture2D Texture;
    public static Texture2D ObjectTilemap;
    public static int TILESIZE = 16;
    private Vector2 _position;
    private Vector2 _imagePos;
    private Vector2 _textPosition;
    public Vector2 Position {
        // adjusts button position to centre of button bar
        set {
            _position = new Vector2(value.X - (_width / 2), value.Y - (_height / 2));
            SetImagePos();
        }
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
    private Rectangle _sourceRectangle;
    public int Id;
    private Rectangle _buttonBox {
        // produces a bounding box for detecting when mouse is over button 
        get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height); }
    }
    private bool _mouseReleased = true;

    public Button(int width, int height, string text, int id = -1) {
        _width = width;
        _height = height;
        _text = text;
        Id = id;
        // if id is not 0 or -1
        // then it is image button so get source from texture
        if (Id != -1 && Id != 0) {
            _sourceRectangle = new Rectangle((Id - 1) * TILESIZE, 0, TILESIZE, TILESIZE);
        }
    }

    private void SetImagePos() {
        // correctly positions images in centre of button
        if (Id != -1 && Id != 0) {
            _imagePos = new Vector2(_position.X + _width / 2 - 16, _position.Y + _height / 2 - 16);
        }
    }

    public event EventHandler Clicked;

    public override void Update(GameTime gameTime) {
        MouseState previousMouse = Game1.PreviousMouse;
        MouseState currentMouse = Game1.CurrentMouse;

        if (previousMouse.LeftButton == ButtonState.Released) {
            _mouseReleased = true;
        }

        // checks if the mouse is over the button
        // then checks if the left mouse button changes state, and thus is clicked
        if (_buttonBox.Contains(new Vector2(currentMouse.X, currentMouse.Y)) &&
            previousMouse.LeftButton == ButtonState.Released &&
            currentMouse.LeftButton == ButtonState.Pressed && _mouseReleased) {
            Clicked?.Invoke(this, new EventArgs());
        }
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, _buttonBox, new Color(59, 93, 201));
        if (Id == -1 || Id == 0) {
            spriteBatch.DrawString(ButtonFont, _text, _textPosition, Color.White);
        }
        else {
            spriteBatch.Draw(ObjectTilemap, _imagePos, _sourceRectangle, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
        }
    }
}
