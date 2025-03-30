using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class TextBox : GameObject {

    private SpriteFont _font;
    public static Texture2D Background;
    private Rectangle _box;
    private string _text = "";
    public string Text { get { return _text; } }

    public TextBox(Vector2 position, int width, int height, SpriteFont font) {
        _box = new Rectangle((int)position.X, (int)position.Y, width, height);
        _font = font;
    }

    public void HandleTextInput(object sender, TextInputEventArgs e) {
        // checks for when the player presses backspace
        // and if there are characters to remove, then removes last character
        if (e.Character == '\b' && _text.Length > 0) {
            _text = _text.Substring(0, _text.Length - 1);
        }
        // adds character if the max length isnt reached 
        // and the character is alphanumeric
        else if (_text.Length < 16 && char.IsLetterOrDigit(e.Character)) {
            _text += e.Character;
        }
    }

    public override void Update(GameTime gameTime) {

    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Background, _box, new Color(59, 93, 201));
        spriteBatch.DrawString(_font, _text, new Vector2(_box.X + 10, _box.Y + _box.Height / 5), Color.White);
    }
}