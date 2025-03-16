using System;

using Microsoft.Xna.Framework;

namespace CS_Coursework;

public class GameplayObjectManager {
    private static Tuple<int, int> _gridSize = new Tuple<int, int>(48, 20);
    // grid of ints representing the current state of the level
    private int[,] _levelIds = new int[_gridSize.Item1, _gridSize.Item2];
    // grid holding the actual objects
    private LevelObject[,] _levelObjects = new LevelObject[_gridSize.Item1, _gridSize.Item2];
    public LevelObject[,] LevelObjects { get { return _levelObjects; } }

    public void PopulateCells(Gamestate levelGameplay, int[,] levelIds, PlayerCharacter playerCharacter) {
        bool valid = CheckLevelValidity(levelGameplay, levelIds);
        // goes back to previous menu if level not valid
        if (!valid) {
            GamestateManager.RemoveGamestate();
        }

        _levelIds = levelIds;

        // loops across list of ids to populate levelObjects array
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                int id = _levelIds[i, j];
                Vector2 position = Grid.GetCellPosition(new Vector2(i, j));

                // default levelObject in case anything goes wrong, so program doesn't crash
                LevelObject levelObject = new LevelObject(position, id, false, false);

                // creates specific levelObjects based on id
                switch (id) {
                    case 1:
                        levelObject = new BasicSurface(position, id, true, true);
                        break;
                    case 2:
                        levelObject = new IceSurface(position, id, true, true);
                        break;
                    case 3:
                        // creates door, collides, is impactable
                        levelObject = new SpringSurface(position, id, true, true);
                        break;
                    case 4:
                        // creates spawn point does not collide is not impactable
                        levelObject = new LevelObject(position, id, false, false);
                        playerCharacter.SetSpawn(position);
                        break;
                    case 5:
                        // creates key, collides, but is not impactable
                        levelObject = new Key(position, id, true, false);
                        break;

                }

                _levelObjects[i, j] = levelObject;
                levelGameplay.AddObject(levelObject);
            }
        }
    }

    // checks if the level meets all the requirements
    private bool CheckLevelValidity(Gamestate levelGameplay, int[,] levelIds) {
        int spawnCount = 0;
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                switch (levelIds[i, j]) {
                    // counts number of spawn points
                    case 4:
                        spawnCount++;
                        break;
                }
            }
        }

        // level not valid if there is anything other than 1 spawn point
        if (spawnCount != 1) {
            return false;
        }
        return true;
    }
}
