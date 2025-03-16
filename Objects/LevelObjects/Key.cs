using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class Key : LevelObject {
    private const int COLLIDERSIZE = 16;

    public Key(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) {
        _boundingBox = new BoundingBox(position + new Vector2(8, 8), new Vector2(COLLIDERSIZE, COLLIDERSIZE));
    }

    public override void OnCollision(PlayerCharacter player) {
        // checks if the player is colliding with the key
        if (player.BoundingBox.Intersects(_boundingBox) && player.HasKey == false) {
            // if so hide the key
            Visible = false;
            player.HasKey = true;
        }
    }
}