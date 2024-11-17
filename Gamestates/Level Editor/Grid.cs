using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public static class Grid
{
    private static Vector2 _position = new Vector2(20, 90);
    private static Vector2 _cellSize = new Vector2(40, 40);
    private static Vector2 _currentCell;
    private static Rectangle _gridRect = new Rectangle((int)_position.X, (int)_position.Y, 1240, 640);
    private static MouseState _mouseState;
    public static Highlighter Highlighter = new Highlighter();

    public static void UpdateGrid()
    {
        _mouseState = Mouse.GetState();
        
        // checks if the mouse is actually in the editor area
        if (_gridRect.Contains(_mouseState.Position))
        {
            GridMouseSnap();
            HighlightCell();
        }
        else{
            Highlighter.Enabled = false;
        }
    }

    private static void GridMouseSnap()
    {
        Point mousePos = _mouseState.Position;

        // finds the grid position the mouse is in
        int mouseColumn = (mousePos.X - (int)_position.X) / (int)_cellSize.X;
        int mouseRow = (mousePos.Y - (int)_position.Y) / (int)_cellSize.Y;
        _currentCell = new Vector2(mouseColumn, mouseRow);
        Console.WriteLine(_currentCell); 
    }

    private static void HighlightCell()
    {
        // enables the highlighter and places it in the correct position
        Highlighter.Enabled = true;
        Highlighter.Position = _position + new Vector2(_currentCell.X * _cellSize.X, _currentCell.Y * _cellSize.Y);
    }
}
