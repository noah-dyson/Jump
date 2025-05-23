using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class ButtonBar : GameObject {
    public static Texture2D Texture;
    public List<Button> Buttons;
    private Vector2 _position;
    private int _width;
    private int _height;

    public ButtonBar(Vector2 position, int width, int height) {
        _position = position;
        _width = width;
        _height = height;
    }

    public override void Update(GameTime gameTime) {
        foreach (Button button in Buttons) {
            button.Update(gameTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), new Color(41, 54, 111));
        foreach (Button button in Buttons) {
            button.Draw(spriteBatch);
        }
    }

    public void SetButtonPositions() {
        // gap represents the distance between the centres of each button
        // offset is the amount each button centre is from the left side of the button bar
        float gap = (float)_width / Buttons.Count;
        float offset = gap / 2;

        foreach (Button button in Buttons) {
            // sets position of both the button and its text so their left corners are at the centre
            // this is then adjust within the button class
            button.Position = button.TextPosition = new Vector2(_position.X + offset, _position.Y + _height / 2);
            offset += gap;
        }
    }
}
