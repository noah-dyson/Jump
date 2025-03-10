using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class Key : LevelObject {
    private const int COLLIDERSIZE = 16;

    public Key(Vector2 position, int id) : base(position, id, false) {
        _boundingBox = new BoundingBox(position + new Vector2(8, 8), new Vector2(COLLIDERSIZE, COLLIDERSIZE));
    }

    public bool CheckCollision(PlayerCharacter _playerCharacter) {
        if (_playerCharacter.BoundingBox.Intersects(_boundingBox)) {
            Visible = false;
            return true;
        }
        else {
            return false;
        }
    }
}