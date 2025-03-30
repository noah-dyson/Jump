using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class KeyIcon : GameObject {
    public static Texture2D Texture;
    private Vector2 _position;
    private bool _enabled = false;
    public bool Enabled { set { _enabled = value; } }

    public KeyIcon(Vector2 position) {
        _position = position;
    }
    public override void Update(GameTime gameTime) {
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (!_enabled) {
            spriteBatch.Draw(Texture, _position, null, Color.Black, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
        }
        else {
            spriteBatch.Draw(Texture, _position, null, Color.White, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
        }
    }
}