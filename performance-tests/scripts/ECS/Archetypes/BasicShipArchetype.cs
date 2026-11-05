using Godot;
using System;
using System.Collections.Generic;

namespace ECS;
public struct BasicShipArchetype() : IArchetype
{
    public readonly Dictionary<ComponentType, Component> ArchetypeComponents => new()
    {
        {ComponentType.Position, new PositionComponent()},
        {ComponentType.GridPosition, new GridPositionComponent()},
        {ComponentType.Rotation, new RotationComponent()},
        {ComponentType.Size, new SizeComponent()},
        {ComponentType.Health, new HealthComponent()
        {
            Health = 100
        }
        },
        {ComponentType.Damage, new DamageComponent()},
        {ComponentType.Team, new TeamComponent()},
        {ComponentType.Behaviour, new BehaviourComponent()},
        {ComponentType.Goal, new GoalComponent()}
    };
}
