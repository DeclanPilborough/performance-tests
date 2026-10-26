using Godot;
using System;

namespace ECS;

/// <summary>
/// System for selecting targets for ships without them already.
/// </summary>
public partial class TargetingSystem : System
{
    public override ComponentType ComponentFilter => throw new NotImplementedException();

    public override void Run()
    {
        throw new NotImplementedException();
    }

}
