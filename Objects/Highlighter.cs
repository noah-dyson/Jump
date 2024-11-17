using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class Highlighter : GameObject
{
    public static Texture2D Texture;
    public bool Enabled = false;
    public Vector2 Position;

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Enabled)
            spriteBatch.Draw(Texture, Position, null, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
    }

    public override void Update(GameTime gameTime)
    {

    }
}
