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
            Health = 1000
        }
        },
        {ComponentType.Damage, new DamageComponent()
        {
            DamagePerShot = 10,
            ShotRechargeTime = 0.1f
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
            MovementSpeed = 50,
            RotationSpeed = 1
        }
        },
        {ComponentType.Goal, new GoalComponent()}
    };
}
