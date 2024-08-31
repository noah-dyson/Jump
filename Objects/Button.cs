using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Button : GameObject
{
    public static SpriteFont ButtonFont;
    public static Texture2D Texture;
    private Vector2 _position;
    public Vector2 Position 
    {
        set { _position = new Vector2(value.X - (_width/2), value.Y - (_height/2)); } 
    }
    private int _width;
    private int _height;
    private string _text;
    private MouseState _previousMouse;
    private MouseState _currentMouse;
    private Rectangle buttonBox
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _width, _height); }
    }

    public Button(int width, int height, string text)
    {
        _width = width;
        _height = height;
        _text = text;
    }

    public event EventHandler OnClick;

    public override void Update(GameTime gameTime)
    {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();


        if (buttonBox.Contains(new Vector2(_currentMouse.X, _currentMouse.Y)) &&
            _previousMouse.LeftButton == ButtonState.Released &&
            _currentMouse.LeftButton == ButtonState.Pressed)
        {
            OnClick.Invoke(this, new EventArgs());
        } 
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, buttonBox, Color.White);
        spriteBatch.DrawString(ButtonFont, _text, _position, Color.White);
    }
}