using Godot;
using System;

namespace ECS;
public partial class SizeComponent : Component
{
    // height and width in world units
    public float Height = 0.0f;
    public float Width = 0.0f;
}
