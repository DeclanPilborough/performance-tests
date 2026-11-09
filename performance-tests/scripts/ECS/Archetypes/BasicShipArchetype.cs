using Godot;
using System;
using System.Collections.Generic;

namespace ECS;
public struct BasicShipArchetype() : IArchetype
{
    public static Dictionary<ComponentType, Component> ArchetypeComponents => new()
    {
        {ComponentType.Position, new PositionComponent()},
        {ComponentType.GridPosition, new GridPositionComponent()},
        {ComponentType.Rotation, new RotationComponent()},
        {ComponentType.Size, new SizeComponent()
        {
            Height = 1,
            Width = 1
        }
        },
        {ComponentType.Health, new HealthComponent()
        {
            Health = 100
        }
        },
        {ComponentType.Damage, new DamageComponent()
        {
            DamagePerShot = 10,
            ShotRechargeTime = 1.5f
        }
        },
        {ComponentType.Team, new TeamComponent()},
        {ComponentType.Targeting, new TargetingComponent()
        {
            TargetPingRechargeTime = 1
        }
        },
        {ComponentType.Behaviour, new BehaviourComponent()
        {
            MovementSpeed = 10,
            RotationSpeed = 1
        }
        },
        {ComponentType.Goal, new GoalComponent()}
    };

    public Dictionary<ComponentType, Component> GetArchetypeComponents()
    {
        return new Dictionary<ComponentType, Component>(ArchetypeComponents);
    }
}
