using Godot;
using System;

namespace ECS;

/// <summary>
/// System that calculates the grid position of all ships according to their position and size.
/// 
/// Unfortunately there I have not thought of a better way than rebuilding the grid every frame.
/// </summary>
public partial class GridSystem : System
{
    public override ComponentType ComponentFilter => throw new NotImplementedException();

    public override void Run()
    {
        throw new NotImplementedException();
    }

}
