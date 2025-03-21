using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class LevelObjectManager {
    private static Tuple<int, int> _gridSize = new Tuple<int, int>(48, 20);
    // grid of ints representing the current state of the level
    private EditorObject[,] _editorObjects = new EditorObject[_gridSize.Item1, _gridSize.Item2];
    public EditorObject[,] EditorObjects { get { return _editorObjects; } }
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

    // places an object in the scene by changing id of editor object
    private void PlaceObject(Vector2 currentCell, int currentObject) {
        _editorObjects[(int)currentCell.X, (int)currentCell.Y].Id = currentObject;
    }

    // removes an object by setting id of editor object back to 0
    private void DeleteObject(Vector2 currentCell) {
        _editorObjects[(int)currentCell.X, (int)currentCell.Y].Id = 0;
    }

    // generates all the editor objects and adds them as objects to the gamestate
    public void CreateLevelObjects(Gamestate levelEditor) {
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                int id = 0;
                // GetCellPosition converts from cell coordinate to pixel coordinate.
                Vector2 position = Grid.GetCellPosition(new Vector2(i, j));

                // creates all the level objects as id 0 so empty
                EditorObject editorObject = new EditorObject(position, id);
                _editorObjects[i, j] = editorObject;
                levelEditor.AddObject(editorObject);
            }
        }
    }

    // sets the levelobjects array and cell-labels when a level is loaded.
    public void LoadNewLevelObjects(int[,] levelIds) {
        for (int i = 0; i < _gridSize.Item1; i++) {
            for (int j = 0; j < _gridSize.Item2; j++) {
                // _cellLabels[i, j].Text = _editorObjects[i, j].ToString();
                _editorObjects[i, j].Id = levelIds[i, j];
            }
        }
    }
}
