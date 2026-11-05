using Godot;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// Uses position and casualty data to calculate a goal/goals for each team.
/// 
/// May use more/different data later down the line to make smarter battle decisions.
/// </summary>
public partial class GoalSystem : System
{
    public override ComponentType ComponentFilter => ComponentType.Team | ComponentType.Position;

    public override void Run(double delta)
    {
        List<int> dataEntities = world.GetEntitiesByComponents(ComponentFilter);

        // calculates average centre of all ships for each team

        float t0TotalX = 0;
        float t0TotalY = 0;
        int t0Count = 0;

        float t1TotalX = 0;
        float t1TotalY = 0;
        int t1Count = 0;

        List<GoalComponent> t0Goals = [];
        List<GoalComponent> t1Goals = [];

        foreach (int entity in dataEntities)
        {
            Dictionary<ComponentType, Component> entityComponents = world.GetComponentsOfEntity(entity);

            switch ((entityComponents[ComponentType.Team] as TeamComponent).Team)
            {
                case 0: 
                    {
                        Vector2 entityPosition = (entityComponents[ComponentType.Position] as PositionComponent).Position;
                        t0TotalX += entityPosition.X;
                        t0TotalY += entityPosition.Y;
                        t0Count++;
                        t0Goals.Add(entityComponents[ComponentType.Goal] as GoalComponent);
                        break;
                    }

                case 1: 
                    {
                        Vector2 entityPosition = (entityComponents[ComponentType.Position] as PositionComponent).Position;
                        t1TotalX += entityPosition.X;
                        t1TotalY += entityPosition.Y;
                        t1Count++;
                        t1Goals.Add(entityComponents[ComponentType.Goal] as GoalComponent);
                        break;
                    }
            }
        }

        Vector2 t0AvgPos = new(t0TotalX/t0Count, t0TotalY/t0Count);
        Vector2 t1AvgPos = new(t1TotalX/t1Count, t1TotalY/t1Count);

        (int, int) t0AvgGridPos = world.GetGridCellFromPosition(t0AvgPos);
        (int, int) t1AvgGridPos = world.GetGridCellFromPosition(t1AvgPos);

        // tell each ship from each team where to go

        foreach (GoalComponent goal in t0Goals)
        {
            goal.Goal = t1AvgGridPos;
        }

        foreach (GoalComponent goal in t1Goals)
        {
            goal.Goal = t0AvgGridPos;
        }
    }
}
