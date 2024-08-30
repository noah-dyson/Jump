using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class TestGameObject : GameObject
{
    private Vector2 _position;
    private float _scale;
    private Texture2D _texture;
    public TestGameObject(Vector2 position, float scale, Texture2D texture)
    {
        _position = position;
        _scale = scale;
        _texture = texture;
    }
    public override void Update(GameTime gameTime)
    {
        _position = new Vector2(_position.X+1, _position.Y+1);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0); 
    }

}