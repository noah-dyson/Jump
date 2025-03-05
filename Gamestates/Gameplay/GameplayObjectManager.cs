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

    public void PopulateCells(Gamestate levelGameplay, int[,] levelIds) {
        _levelIds = levelIds;

        // loops across list of ids to populate levelObjects array
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                int id = _levelIds[i, j];
                Vector2 position = Grid.GetCellPosition(new Vector2(i, j));

                // default levelObject in case anything goes wrong, so program doesn't crash
                LevelObject levelObject = new LevelObject(position, id);

                // creates specific levelObjects based on id
                switch (id) {
                    case 1:
                        levelObject = new BasicSurface(position, id);
                        break;
                }

                _levelObjects[i, j] = levelObject;
                levelGameplay.AddObject(levelObject);
            }
        }
    }
}
