using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public static class Grid
{
    private static Vector2 _position = new Vector2(20, 90);
    private static Vector2 _cellSize = new Vector2(40, 40);
    private static Vector2 _currentCell;
    private static Rectangle _gridRect = new Rectangle((int)_position.X, (int)_position.Y, (int)_cellSize.X, (int)_cellSize.Y);

    private static MouseState _mouseState;

    public static void UpdateGrid()
    {
        _mouseState = Mouse.GetState();
        GridMouseSnap();
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
}