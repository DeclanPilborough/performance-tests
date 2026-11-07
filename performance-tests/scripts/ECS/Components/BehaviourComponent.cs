using Godot;
using System;

namespace ECS;
public partial class BehaviourComponent : Component
{
    // determines movement behaviour
    public float MovementSpeed = 0.0f;
    public float RotationSpeed = 0.0f;
}
