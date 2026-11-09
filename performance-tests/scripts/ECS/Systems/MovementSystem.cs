using Godot;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// A system that moves every ship in the world during the simulation frame.
/// 
/// May be broken down into mutliple subsystems dependant on requirements.
/// </summary>
public partial class MovementSystem : System
{
    public override ComponentType ComponentFilter => ComponentType.Position | ComponentType.Rotation | ComponentType.Goal | ComponentType.Behaviour;

    public override void Run(double delta)
    {
        List<int> entities = world.GetEntitiesByComponents(ComponentFilter);
        if (entities.Count == 0) { return; }
        GD.Print($"MovementSystem running with {entities.Count} entities");

        // for now, all ships behave the same-ish way
        foreach (int entity in entities)
        {
            Dictionary<ComponentType, Component> components = world.GetComponentsOfEntity(entity);

            Vector2 moveToward = new();

            // if the ship has a target, turn and move toward that instead of goal
            if (components.TryGetValue(ComponentType.Target, out Component value))
            {
                // I feel sick
                moveToward = (world.GetComponentsOfEntity((value as TargetComponent).TargetEntityID)[ComponentType.Position] as PositionComponent).Position; 
            }
            else // if no target, move toward goal
            {
                moveToward = world.worldGrid.GetGridCellCentre((components[ComponentType.Goal] as GoalComponent).Goal);
            }

            PositionComponent position = components[ComponentType.Position] as PositionComponent;
            RotationComponent rotation = components[ComponentType.Rotation] as RotationComponent;
            BehaviourComponent behaviour = components[ComponentType.Behaviour] as BehaviourComponent;

            // calculate angle to turn towards
            float angle = Mathf.Atan2(moveToward.Y - position.Position.Y, moveToward.X - position.Position.X);

            // turn towards angle with speed
            rotation.Rotation = Mathf.LerpAngle(rotation.Rotation, angle, behaviour.RotationSpeed * (float)delta);

            // get forward vector
            Vector2 forward = new(Mathf.Cos(rotation.Rotation), Mathf.Sin(rotation.Rotation));

            // move toward forward vector, using speed
            forward *= behaviour.MovementSpeed * (float)delta;

            position.Position += forward;

            position.Position = position.Position.Clamp(Vector2.Zero, new(5000, 5000)); // clamp movement to dummy grid values (to prevent errors)

            // GD.Print($"Diagnostics:\nMoving Toward {moveToward} from {position.Position}\n" +
            // $"Rotation From {rotation.Rotation} to {angle} by amount {behaviour.RotationSpeed * (float)delta}\n" +
            // $"Moving forward with vector {forward} by amount {behaviour.MovementSpeed * (float)delta}");
        }
    }

}
