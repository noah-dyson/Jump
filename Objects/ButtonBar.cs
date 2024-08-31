using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class ButtonBar : GameObject
{
    public static Texture2D Texture;
    public List<Button> Buttons = new List<Button>();
    private Vector2 _position;
    private int _width;
    private int _height;

    public ButtonBar(Vector2 position, int width, int height)
    {
        _position = position;
        _width = width;
        _height = height;
    }

    public override void Update(GameTime gameTime)
    {
        foreach (Button button in Buttons)
        {
            button.Update(gameTime);
        } 
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
        foreach (Button button in Buttons)
        {
            button.Draw(spriteBatch);
        }
    }

    public void SetButtonPositions()
    {
        int gap = _width / Buttons.Count;
        int offset = gap/2;

        foreach (Button button in Buttons)
        {
            button.Position = new Vector2(_position.X + offset, _position.Y + _height / 2);
            offset += gap;
        }
    }
}