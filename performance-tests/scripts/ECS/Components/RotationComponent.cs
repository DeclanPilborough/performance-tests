using Godot;
using System;

namespace ECS;
public partial class RotationComponent : Component
{
    // rotation in radians relative to parent
    public float Rotation = 0.0f;
}
