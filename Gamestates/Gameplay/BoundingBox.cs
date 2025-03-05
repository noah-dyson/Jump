using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class BoundingBox {
    public Vector2 Position;
    private Vector2 _size;
    public Vector2 Size { get { return _size; } }

    public BoundingBox(Vector2 position, Vector2 size) {
        Position = position;
        _size = size;
    }

    public bool Intersects(BoundingBox targetBox) {

        if ((Position.X < targetBox.Position.X + targetBox.Size.X &&
            Position.X + _size.X > targetBox.Position.X) &&
            (Position.Y < targetBox.Position.Y + targetBox.Size.Y &&
            Position.Y + _size.Y > targetBox.Position.Y)) {
            return true;
        }
        return false;
    }
}