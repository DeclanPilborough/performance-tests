using Godot;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// System for selecting targets for ships without them already.
/// </summary>
public partial class TargetingSystem : System
{
    public override ComponentType ComponentFilter => ComponentType.Position | ComponentType.GridPosition | ComponentType.Targeting | ComponentType.Team;

    public override void Run(double delta)
    {
        List<int> entities = world.GetEntitiesByComponents(ComponentFilter);

        foreach (int entity in entities)
        {
            Dictionary<ComponentType, Component> components = world.GetComponentsOfEntity(entity);
            TargetingComponent targeting = components[ComponentType.Targeting] as TargetingComponent;

            targeting.PingRecharge += (float)delta;

            if (targeting.PingRecharge >= targeting.TargetPingRechargeTime)
            {
                // ping for targets
                List<int> potentialTargets = world.worldGrid.GetEntitiesInNeighbours((components[ComponentType.GridPosition] as GridPositionComponent).GridPosition);

                TeamComponent team = components[ComponentType.Team] as TeamComponent;

                    int target = -1; // -1 for no target, yet
                    float targetDistance = 1000f; // should start at max range, if that will ever exist
                
                // remove allies from consideration, and find the closest enemy
                foreach (int potentialTarget in potentialTargets)
                {
                    Dictionary<ComponentType, Component> potentialTargetComponents = world.GetComponentsOfEntity(potentialTarget);

                    if ((potentialTargetComponents[ComponentType.Team] as TeamComponent).Team != team.Team)
                    {
                        float distance = (components[ComponentType.Position] as PositionComponent).Position.DistanceSquaredTo((potentialTargetComponents[ComponentType.Position] as PositionComponent).Position);
                        if (distance < targetDistance)
                        {
                            target = potentialTarget;
                            targetDistance = distance;
                        }
                    }
                }

                // target acquired
                if (target != -1)
                {
                    world.AddComponentToEntity(entity, ComponentType.Target, new TargetComponent
                    {
                        TargetEntityID = target
                    });
                    world.RemoveComponentFromEntity(entity, ComponentType.Targeting);

                    continue;
                }

                targeting.PingRecharge = 0f;
            }
        }
    }

}
