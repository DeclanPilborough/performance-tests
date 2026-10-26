using Godot;
using System;

namespace ECS;

/// <summary>
/// Uses position and casualty data to calculate a goal/goals for each team.
/// 
/// May use more/different data later down the line to make smarter battle decisions.
/// </summary>
public partial class GoalSystem : System
{
    public override ComponentType ComponentFilter => throw new NotImplementedException();

    public override void Run()
    {
        throw new NotImplementedException();
    }

}
