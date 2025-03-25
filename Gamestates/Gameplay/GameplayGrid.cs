using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class GameplayGrid {
    // defines the position of the top left of the grid
    private static Vector2 _position = new Vector2(32, 90);

    // each cell is 32 pixels by 32 pixels
    private static Vector2 _cellSize = new Vector2(32, 32);

    // rectangle covering the whole grid size, which is 1536x640 pixels so 48x20
    public static Rectangle GridRect = new Rectangle(
        (int)_position.X,
        (int)_position.Y,
        1536,
        640
    );

    public static Vector2 GetCellPosition(Vector2 cell) {
        Vector2 cellPosition = new Vector2(cell.X * _cellSize.X, cell.Y * _cellSize.Y) + _position;
        return cellPosition;
    }

    public void UpdateGrid(LevelObjectManager levelObjectManager) {

    }
}
