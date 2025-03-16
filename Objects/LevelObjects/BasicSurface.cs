using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class BasicSurface : LevelObject {

    public BasicSurface(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }
}