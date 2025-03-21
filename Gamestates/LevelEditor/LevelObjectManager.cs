using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class LevelObjectManager {
    private static Tuple<int, int> _gridSize = new Tuple<int, int>(48, 20);
    // grid of ints representing the current state of the level
    private EditorObject[,] _levelObjects = new EditorObject[_gridSize.Item1, _gridSize.Item2];
    public EditorObject[,] LevelObjects { get { return _levelObjects; } }
    // grid of cell label objects that show the current state of the cell for testing
    private CellLabel[,] _cellLabels = new CellLabel[_gridSize.Item1, _gridSize.Item2];
    private MouseState _previousMouse;
    private MouseState _currentMouse;
    private Gamestate _levelEditor;

    // called every update cycle and checks whether object is placed or deleted
    public void EditLevelCheck(Vector2 currentCell, int currentObject) {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();

        // checks whether the left mouse button has been released in this frame
        if (_previousMouse.LeftButton == ButtonState.Pressed) {
            PlaceObject(currentCell, currentObject);
        }
        if (_previousMouse.RightButton == ButtonState.Pressed) {
            DeleteObject(currentCell);
        }
    }

    // places an object in the scene by changing value in grid
    private void PlaceObject(Vector2 currentCell, int currentObject) {
        _levelEditor.RemoveObject(_levelObjects[(int)currentCell.X, (int)currentCell.Y]);
        EditorObject newObject = new EditorObject(Grid.GetCellPosition(currentCell), currentObject);
        _levelObjects[(int)currentCell.X, (int)currentCell.Y] = newObject;
        _levelEditor.AddObject(newObject);
        // _cellLabels[(int)currentCell.X, (int)currentCell.Y].Text = Convert.ToString(currentObject);
    }

    // removes an object by setting its value in the grid back to 0
    private void DeleteObject(Vector2 currentCell) {
        _levelEditor.RemoveObject(_levelObjects[(int)currentCell.X, (int)currentCell.Y]);
        EditorObject newObject = new EditorObject(Grid.GetCellPosition(currentCell), 0);
        _levelObjects[(int)currentCell.X, (int)currentCell.Y] = newObject;
        _levelEditor.AddObject(newObject);
        // _cellLabels[(int)currentCell.X, (int)currentCell.Y].Text = "0";
    }

    // generates all the cell labels and adds them as objects to the gamestate
    public void CreateCellLabels(Gamestate levelEditor) {
        _levelEditor = levelEditor;
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                int id = 0;
                // GetCellPosition converts from cell coordinate to pixel coordinate.
                //CellLabel cellLabel = new CellLabel(id, Grid.GetCellPosition(new Vector2(i, j)) + new Vector2(10, 10));
                //_cellLabels[i, j] = cellLabel;
                //levelEditor.AddObject(cellLabel);

                Vector2 position = Grid.GetCellPosition(new Vector2(i, j));

                // default levelObject in case anything goes wrong, so program doesn't crash
                EditorObject levelObject = new EditorObject(position, id);
                _levelObjects[i, j] = levelObject;
                levelEditor.AddObject(levelObject);
            }
        }
    }

    // sets the levelobjects array and cell-labels when a level is loaded.
    public void LoadNewLevelObjects(int[,] levelObjects) {
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                // _cellLabels[i, j].Text = _levelObjects[i, j].ToString();
                EditorObject levelObject = new EditorObject(Grid.GetCellPosition(new Vector2(i, j)), levelObjects[i, j]);
                _levelObjects[i, j] = levelObject;
                _levelEditor.AddObject(levelObject);
            }
        }
    }
}
