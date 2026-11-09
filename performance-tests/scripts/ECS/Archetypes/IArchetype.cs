using Godot;
using System;
using System.Collections.Generic;

namespace ECS;
public interface IArchetype
{
    public static Dictionary<ComponentType, Component> ArchetypeComponents { get; }

    public Dictionary<ComponentType, Component> GetArchetypeComponents();
}
