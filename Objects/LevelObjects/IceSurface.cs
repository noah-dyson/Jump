using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class IceSurface : LevelObject {
    public IceSurface(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }

    public override void OnCollisionVertical(PlayerCharacter player) {
        // sets the friction to be lower
        player.FrictionalForce = 0.1f;
    }
}