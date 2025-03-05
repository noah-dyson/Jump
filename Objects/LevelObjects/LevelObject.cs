using System.Data.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class LevelObject : GameObject {
    private const int TILESIZE = 16;
    private const int SCALE = 2;

    public static Texture2D TileMap;
    private BoundingBox _boundingBox;
    public BoundingBox BoundingBox { get { return _boundingBox; } }
    private Vector2 _position;
    private Rectangle _sourceRectangle;
    private int _id;
    public bool Visible = true;

    public LevelObject(Vector2 position, int id) {
        _position = position;
        _id = id;

        _boundingBox = new BoundingBox(_position, new Vector2(TILESIZE * SCALE, TILESIZE * SCALE));
        SetSource();
    }

    private void SetSource() {
        if (_id == 0) {
            Visible = false;
        }
        else {
            // gets portion of tilemap to draw from, 1 subtracted from id because 0 represents no object
            _sourceRectangle = new Rectangle((_id - 1) * TILESIZE, 0, TILESIZE, TILESIZE);
        }
    }

    public override void Update(GameTime gameTime) { }

    public override void Draw(SpriteBatch spriteBatch) {
        if (Visible) {
            spriteBatch.Draw(TileMap, _position, _sourceRectangle, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
        }
    }
}