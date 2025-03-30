using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class Door : LevelObject {
    public Door(Vector2 position, int id, bool collides, bool impactable) : base(position, id, collides, impactable) { }

    public override void OnCollision(PlayerCharacter player) {
        if (player.HasKey) {

            LevelGameplay levelGameplay = (LevelGameplay)GamestateManager.CurrentGamestate();
            levelGameplay.UpdateTime();
            // level completed
            // will be replaced by a menu later so is not so abrupt
            GamestateManager.RemoveGamestate();
        }
    }
}