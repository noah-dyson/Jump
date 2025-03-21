using Microsoft.Xna.Framework;

using System;

namespace CS_Coursework;

public class SpringSurface : LevelObject {
    public SpringSurface(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }

    public override void OnCollisionVertical(PlayerCharacter player) {
        // equivalent to checking if the player is on the ground
        if (Math.Abs(player.InitialVelocity.Y) < 1f) {
            player.MaxHorizontalSpeed = 3;
        }
        // if not on ground bounce player
        else if (player.NextVelocity == Vector2.Zero) {
            player.NextVelocity.Y = -player.InitialVelocity.Y * 2f;
        }
    }
}
