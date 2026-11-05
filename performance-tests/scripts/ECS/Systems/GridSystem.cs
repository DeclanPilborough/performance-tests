using Godot;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// System that calculates the grid position of all ships according to their position and size.
/// 
/// Unfortunately there I have not thought of a better way than rebuilding the grid every frame.
/// This may actually be a good opportunity for the use of a quadtree, but that is fairly complex so no.
/// </summary>
public partial class GridSystem : System
{
    public override ComponentType ComponentFilter => ComponentType.Position | ComponentType.GridPosition | ComponentType.Rotation | ComponentType.Size;

    public override void Run(double delta)
    {
        List<int> entities = world.GetEntitiesByComponents(ComponentFilter);

        // clear the grid for rebuilding
        world.worldGrid.ClearEntitiesFromGrid();

        // for now, does not factor in size
        foreach (int entity in entities)
        {
            Dictionary<ComponentType, Component> components = world.GetComponentsOfEntity(entity);
            PositionComponent position = components[ComponentType.Position] as PositionComponent;
            GridPositionComponent gridPosition = components[ComponentType.GridPosition] as GridPositionComponent;

            gridPosition.GridPosition = world.worldGrid.AddEntityToGridCell(entity, position.Position);
        }
    }

}
