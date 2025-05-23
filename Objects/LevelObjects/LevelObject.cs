using System.Data.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class LevelObject : GameObject {
    private const int TILESIZE = 16;
    private const int SCALE = 2;

    public static Texture2D TileMap;
    protected BoundingBox _boundingBox;
    public BoundingBox BoundingBox { get { return _boundingBox; } }
    private Vector2 _position;
    private Rectangle _sourceRectangle;
    private int _id;
    public int Id { get { return _id; } }
    public bool Visible = true;
    private bool _collides;
    public bool Collides { get { return _collides; } }
    private bool _impactable;
    public bool Impactable { get { return _impactable; } }


    public LevelObject(Vector2 position, int id, bool collides, bool impactable) {
        _position = position;
        _id = id;
        _collides = collides;
        _impactable = impactable;

        if (impactable) {
            _boundingBox = new BoundingBox(_position, new Vector2(TILESIZE * SCALE, TILESIZE * SCALE));
        }
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


    public virtual void OnCollision(PlayerCharacter player) { }
    public virtual void OnCollisionHorizontal(PlayerCharacter player) { }
    public virtual void OnCollisionVertical(PlayerCharacter player) { }

    public override void Update(GameTime gameTime) { }

    public override void Draw(SpriteBatch spriteBatch) {
        if (Visible) {
            spriteBatch.Draw(TileMap, _position, _sourceRectangle, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
        }
    }
}