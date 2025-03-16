using Microsoft.Xna.Framework;

using System;
using System.Diagnostics;

namespace CS_Coursework;

public class SpringSurface : LevelObject {
    public SpringSurface(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }

    public override void OnCollisionVertical(PlayerCharacter player) {
        if (Math.Abs(player.InitialVelocity.Y) < 1f) {
            player.MaxHorizontalSpeed = 3;
        }
        else if (player.NextVelocity == Vector2.Zero) {
            player.NextVelocity.Y = -player.InitialVelocity.Y * 2f;
            Debug.WriteLine(player.NextVelocity);
        }
    }
}
