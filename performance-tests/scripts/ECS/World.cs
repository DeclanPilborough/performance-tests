using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS;
public partial class World : RefCounted
{
	public WorldGrid worldGrid;

	int nextID = 0;

	DamageSystem damageSystem;
	GoalSystem goalSystem;
	GridSystem gridSystem;
	MovementSystem movementSystem;
	TargetingSystem targetingSystem;

	ComponentType renderingFilter = ComponentType.Position | ComponentType.Rotation | ComponentType.Health;

	// registers
	Dictionary<int, Dictionary<ComponentType, Component>> EntityComponentRegister;
	Dictionary<ComponentType, List<int>> ComponentEntityRegister;

	public List<int> GetEntitiesByComponents(ComponentType componentFilter)
	{
		HashSet<int> entities = [];
		foreach (ComponentType keyType in ComponentEntityRegister.Keys)
		{
			if (componentFilter == (componentFilter &= keyType))
			{
				entities.UnionWith(ComponentEntityRegister[keyType]);
			}
		}
		return [.. entities];
	}

	public Dictionary<ComponentType, Component> GetComponentsOfEntity(int entityID)
	{
		return EntityComponentRegister[entityID];
	}

	void AddEntity(Dictionary<ComponentType, Component> entityArchetype)
	{
		//GD.Print("Adding Entity");
		// copy components
		Dictionary<ComponentType, Component> newComponents = new(entityArchetype);

		// add components to entity component register
		EntityComponentRegister[nextID] = newComponents;

		// add entity to specific component entity register entry based on total components
		ComponentType key = 0;
		foreach (ComponentType type in newComponents.Keys)
		{
			key |= type;
		}

		if (ComponentEntityRegister.TryGetValue(key, out List<int> value))
		{
            value.Add(nextID);
		}
		else
		{
			ComponentEntityRegister[key] = new(nextID);
		}

		nextID++;
	}
	
	public void AddComponentToEntity(int entityID, ComponentType newComponentType, Component newComponent)
	{
		// get the old ce register key before it is edited
		ComponentType oldKey = 0;
		foreach (ComponentType type in EntityComponentRegister[entityID].Keys)
		{
			oldKey |= type;
		}

		// add the new component to the ec register
		EntityComponentRegister[entityID].Add(newComponentType, newComponent);

		// then remove from old ce register entry and enter into new one
		ComponentEntityRegister[oldKey].Remove(entityID);
		ComponentEntityRegister[oldKey |= newComponentType].Add(entityID);
	}

	public void RemoveComponentFromEntity(int entityID, ComponentType typeToRemove)
	{
		// same as add component but backwards
		ComponentType oldKey = 0;
		foreach (ComponentType type in EntityComponentRegister[entityID].Keys)
		{
			oldKey |= type;
		}

		EntityComponentRegister[entityID].Remove(typeToRemove);

		ComponentEntityRegister[oldKey].Remove(entityID);
		ComponentEntityRegister[oldKey &= ~typeToRemove].Add(entityID);
	}

	// world

	/// <summary>
	/// Starts the world and begins all systems.
	/// </summary>
	public void Start()
	{
		EntityComponentRegister = [];
		ComponentEntityRegister = [];

		worldGrid = new(/* dummy values*/ 50, 100, 100);

		goalSystem = new()
		{
			world = this
		};

		movementSystem = new()
		{
			world = this
		};

		gridSystem = new()
		{
			world = this
		};

		targetingSystem = new()
		{
			world = this
		};

		damageSystem = new()
		{
			world = this
		};
	}

	/// <summary>
	/// Populates the world with the provided entities.
	/// </summary>
	public void PopulateWorld(/*archetypes/data for population*/ Dictionary<int, IArchetype> population)
	{
		worldGrid.BuildGrid();
		
		foreach (KeyValuePair<int, IArchetype> kvp in population)
		{
			for (int i = 0; i < kvp.Key; i++)
			{
				AddEntity(kvp.Value.GetArchetypeComponents());
			}
		}
	}
	
	/// <summary>
	/// Runs a frame of the simulation, including running all systems.
	/// </summary>
	public void RunWorldFrame(double delta) 
	{
		//GD.Print("Running World Frame");
		goalSystem.Run(delta);
		movementSystem.Run(delta);
		gridSystem.Run(delta);
		targetingSystem.Run(delta);
		damageSystem.Run(delta);
	}

	// rendering data

	public RenderingData GetRenderingData()
	{
		List<int> entities = GetEntitiesByComponents(renderingFilter);

		Dictionary<int, Vector2> aliveTransforms = [];
		Dictionary<int, float> aliveRotations = [];
		Dictionary<int, Vector2> deadTransforms = [];

		Dictionary<ComponentType, Component> entityComponents = [];

        foreach (int entityID in entities)
		{
			entityComponents = GetComponentsOfEntity(entityID);

			// if dead, add to dead list
			if ((entityComponents[ComponentType.Health] as HealthComponent).Health <= 0)
			{
				deadTransforms[entityID] = (entityComponents[ComponentType.Position] as PositionComponent).Position;
			}
			// if alive, add pos and rot to alive lists
			else
			{
				aliveTransforms[entityID] = (entityComponents[ComponentType.Position] as PositionComponent).Position;
				aliveRotations[entityID] = (entityComponents[ComponentType.Rotation] as RotationComponent).Rotation;
			}
		}

		// once all data has been collected, create and return struct

		return new()
		{
			liveShipPositions = aliveTransforms,
			liveShipRotations = aliveRotations,
			deadShipPositions = deadTransforms
		};
	}
}

