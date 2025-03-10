using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class Door : LevelObject {
    public Door(Vector2 position, int id) : base(position, id) { }

    public bool CheckCollision(PlayerCharacter _playerCharacter) {
        if (_playerCharacter.BoundingBox.Intersects(_boundingBox)) {
            return true;
        }
        else {
            return false;
        }
    }
}