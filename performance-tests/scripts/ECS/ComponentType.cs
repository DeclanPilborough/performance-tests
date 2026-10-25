using Godot;
using System;

namespace ECS;
[Flags]
public enum ComponentType
{
    // 0 = None, so must start at 1
    Position = 1,
    GridPosition = 1 << 1,
    Rotation = 1 << 2,
    Size = 1 << 3,
    Target = 1 << 4,
    Health = 1 << 5,
    Goal = 1 << 6,
    Team = 1 << 7,
    Damage = 1 << 8,
    Targeting = 1 << 9,
    Behaviour = 1 << 10
}
