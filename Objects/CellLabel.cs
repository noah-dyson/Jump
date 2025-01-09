using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class CellLabel : GameObject {

    public static SpriteFont CellLabelFont;
    private Vector2 _position;
    private string _text;
    public string Text { set { _text = value; } }

    public CellLabel(string text, Vector2 position) {
        _text = text;
        _position = position;
    }

    public override void Update(GameTime gameTime) { }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.DrawString(CellLabelFont, _text, _position, Color.White);
    }
}