using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class Grid {
    // defines the position of the top left of the grid
    private static Vector2 _position = new Vector2(32, 90);

    // each cell is 32 pixels by 32 pixels
    private static Vector2 _cellSize = new Vector2(32, 32);
    public Vector2 CurrentCell;

    // rectangle covering the whole grid size, which is 1536x640 pixels so 48x20
    private static Rectangle _gridRect = new Rectangle(
        (int)_position.X,
        (int)_position.Y,
        1536,
        640
    );
    private MouseState _mouseState;
    public Highlighter Highlighter = new Highlighter();

    public void UpdateGrid(LevelObjectManager levelObjectManager, int currentObject) {
        _mouseState = Mouse.GetState();

        // checks if the mouse is actually in the editor area
        // if so it enables the highlighter
        if (_gridRect.Contains(_mouseState.Position)) {
            GridMouseSnap();
            HighlightCell();
            levelObjectManager.EditLevelCheck(CurrentCell, currentObject);
        }
        else {
            Highlighter.Enabled = false;
        }
    }

    public static Vector2 GetCellPosition(Vector2 cell) {
        Vector2 cellPosition = new Vector2(cell.X * _cellSize.X, cell.Y * _cellSize.Y) + _position;
        return cellPosition;
    }

    private void GridMouseSnap() {
        Point mousePos = _mouseState.Position;

        // finds the grid cell the mouse is in
        int mouseColumn = (mousePos.X - (int)_position.X) / (int)_cellSize.X;
        int mouseRow = (mousePos.Y - (int)_position.Y) / (int)_cellSize.Y;
        CurrentCell = new Vector2(mouseColumn, mouseRow);
    }

    private void HighlightCell() {
        // enables the highlighter and places it in the correct position
        Highlighter.Enabled = true;
        Highlighter.Position =
            _position + new Vector2(CurrentCell.X * _cellSize.X, CurrentCell.Y * _cellSize.Y);
    }
}
