using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ECS;
public partial class WorldGrid : RefCounted
{
    // cell size in world units
    public int cellSize;

    // height and width of grid (size of grid)
    public int gridHeight;
    public int gridWidth;

    Dictionary<(int, int), HashSet<int>> gridCells;

    RandomNumberGenerator rng = new();

    // create the grid
    public void BuildGrid()
    {
        gridCells = [];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridCells[(x, y)] = [];
            }
        }
    }

    public (int, int) GetGridCellFromPosition(Vector2 position)
    {
        return 
        (
            (int)position.X / cellSize,
            (int)position.Y / cellSize
        );
    }

    public Vector2 GetGridCellCentre((int, int) gridCell)
    {
        return new()
        {
            X = (gridCell.Item1 * cellSize) + cellSize / 2,
            Y = (gridCell.Item2 * cellSize) + cellSize / 2,
        };
    }

    public Vector2 GetRandomPositionInGridCell((int, int) gridCell)
    {
        Vector2 min = new()
        {
            X = gridCell.Item1 * cellSize,
            Y = gridCell.Item2 * cellSize
        };

        Vector2 max = new()
        {
            X = min.X + cellSize,
            Y = min.Y + cellSize
        };

        return new()
        {
            X = rng.RandfRange(min.X, max.X),
            Y = rng.RandfRange(min.Y, max.Y)
        };
    }

    // only works with entities smaller than a cell
    public void AddEntityToGridCell(int entityID, Vector2 position)
    {
        gridCells[GetGridCellFromPosition(position)].Add(entityID);
    }

    public List<int> GetEntitiesInGridCell((int, int) gridCell)
    {
        return [.. gridCells[gridCell]];
    }

    // somehow make a way of creating easy-to-use areas/zones/shapes of grid cells surrounding 1
    // for targeting
    // could just start with surrounds
}
