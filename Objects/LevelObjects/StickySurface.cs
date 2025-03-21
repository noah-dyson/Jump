using Microsoft.Xna.Framework;

using System;

namespace CS_Coursework;

public class StickySurface : LevelObject {
    public StickySurface(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }

    public override void OnCollisionVertical(PlayerCharacter player) {
        player.MaxHorizontalSpeed = 3;
        player.VerticalAccel = 1f;
    }
}