using Godot;
using System;

namespace ECS;

/// <summary>
/// A system that moves every ship in the world during the simulation frame.
/// 
/// May be broken down into mutliple subsystems dependant on requirements.
/// </summary>
public partial class MovementSystem : System
{
    public override ComponentType ComponentFilter => throw new NotImplementedException();

    public override void Run()
    {
        throw new NotImplementedException();
    }

}
