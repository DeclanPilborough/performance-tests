using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ECS;
public partial class WorldGrid(int cellSize, int gridHeight, int gridWidth) : RefCounted
{
    // cell size in world units
    //public int cellSize;

    // height and width of grid (size of grid)
    //public int gridHeight;
    //public int gridWidth;

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

    // remove all entities from the grid (to facilitate rebuild)
    public void ClearEntitiesFromGrid()
    {
        foreach ((int, int) cell in gridCells.Keys)
        {
            gridCells[cell] = [];
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
    public (int, int) AddEntityToGridCell(int entityID, Vector2 position)
    {
        (int, int) cell = GetGridCellFromPosition(position);
        gridCells[cell].Add(entityID);
        return cell;
    }

    // public List<int> GetEntitiesInGridCell((int, int) gridCell)
    // {
    //     return [.. gridCells[gridCell]];
    // }

    public HashSet<int> GetEntitiesInGridCell((int, int) gridCell)
    {
        return gridCells[gridCell];
    }

    // somehow make a way of creating easy-to-use areas/zones/shapes of grid cells surrounding 1
    // for targeting
    // could just start with surrounds

    public List<int> GetEntitiesInNeighbours((int, int) gridCell)
    {
        HashSet<int> entities = [];

        for (int x = gridCell.Item1 - 1; x < gridCell.Item1 + 1; x++)
        {
            for (int y = gridCell.Item2 - 1; y < gridCell.Item2 + 1; y++)
            {
                if (x < 0 || x > gridWidth || y < 0 || y > gridHeight)
                {
                    continue;
                }

                entities.UnionWith(gridCells[(x, y)]);
            }
        }
        return [.. entities];
    }
}
