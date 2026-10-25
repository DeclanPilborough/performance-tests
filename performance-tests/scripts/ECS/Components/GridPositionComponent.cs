using Godot;
using System;

namespace ECS;
public partial class GridPositionComponent : Component
{
    public (int, int) GridPosition = (0, 0);
}
