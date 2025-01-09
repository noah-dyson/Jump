using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class LevelObjectManager {
    // grid of ints representing the current state of the level
    private int[,] _levelObjects = new int[31, 16];
    public int[,] LevelObjects { get { return _levelObjects; } }
    // grid of cell label objects that show the current state of the cell for testing
    private CellLabel[,] _cellLabels = new CellLabel[31, 16];
    private MouseState _previousMouse;
    private MouseState _currentMouse;

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
        _levelObjects[(int)currentCell.X, (int)currentCell.Y] = currentObject;
        _cellLabels[(int)currentCell.X, (int)currentCell.Y].Text = Convert.ToString(currentObject);
    }

    // removes an object by setting its value in the grid back to 0
    private void DeleteObject(Vector2 currentCell) {
        _levelObjects[(int)currentCell.X, (int)currentCell.Y] = 0;
        _cellLabels[(int)currentCell.X, (int)currentCell.Y].Text = "0";
    }

    // generates all the cell labels and adds them as objects to the gamestate
    public void CreateCellLabels(Gamestate levelEditor) {
        for (int i = 0; i < 31; i++) {
            for (int j = 0; j < 16; j++) {
                string id = Convert.ToString(_levelObjects[i, j]);
                // GetCellPosition converts from cell coordinate to pixel coordinate.
                CellLabel cellLabel = new CellLabel(id, Grid.GetCellPosition(new Vector2(i, j)) + new Vector2(10, 10));
                _cellLabels[i, j] = cellLabel;
                levelEditor.AddObject(cellLabel);
            }
        }
    }

    // sets the levelobjects array and cell-labels when a level is loaded.
    public void LoadNewLevelObjects(int[,] levelObjects) {
        _levelObjects = levelObjects;
        for (int i = 0; i < 31; i++) {
            for (int j = 0; j < 16; j++) {
                _cellLabels[i, j].Text = _levelObjects[i, j].ToString();
            }
        }
    }
}
