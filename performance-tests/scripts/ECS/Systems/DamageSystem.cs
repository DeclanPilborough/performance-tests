using Godot;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// System for running damage calculations.
/// </summary>
public partial class DamageSystem : System
{
    public override ComponentType ComponentFilter => ComponentType.Target | ComponentType.Damage;

    public override void Run(double delta)
    {
        List<int> entities = world.GetEntitiesByComponents(ComponentFilter);

        foreach (int entity in entities)
        {
            Dictionary<ComponentType, Component> components = world.GetComponentsOfEntity(entity);
            TargetComponent target = components[ComponentType.Target] as TargetComponent;
            DamageComponent damage = components[ComponentType.Damage] as DamageComponent;

            // check if target is dead
            if ((world.GetComponentsOfEntity(target.TargetEntityID)[ComponentType.Health] as HealthComponent).Health <= 0)
            {
                // remove target component and start targeting again
                world.RemoveComponentFromEntity(entity, ComponentType.Target);
                world.AddComponentToEntity(entity, ComponentType.Targeting, new TargetingComponent()
                {
                    // somehow figure out the defaut values of this ship. hrm
                });
                continue;
            }

            // tick down attack timer
            damage.RechargeTimer += (float)delta;

            // attack if attacking
            if (damage.RechargeTimer >= damage.ShotRechargeTime)
            {
                (world.GetComponentsOfEntity(target.TargetEntityID)[ComponentType.Health] as HealthComponent).Health -= damage.DamagePerShot;

                damage.RechargeTimer = 0f;
            }
        }
    }

}
