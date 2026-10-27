using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS;
public partial class World : RefCounted
{
	int nextID = 0;

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
				entities = (HashSet<int>)entities.Concat(ComponentEntityRegister[keyType]);
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
		ComponentEntityRegister[key].Add(nextID);

		nextID++;
	}
	
	void AddComponentToEntity(int entityID, ComponentType newComponentType, Component newComponent)
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

	void RemoveComponentFromEntity(int entityID, ComponentType typeToRemove)
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
	/// Populates the world with the provided entities.
	/// </summary>
	public void PopulateWorld()
	{
		EntityComponentRegister = [];
		ComponentEntityRegister = [];

		// create all entities
	}
	
	/// <summary>
	/// Runs a frame of the simulation, including running all systems.
	/// </summary>
	public void RunWorldFrame() 
	{
		
	}
}

