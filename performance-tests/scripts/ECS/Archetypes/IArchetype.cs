using Godot;
using System;
using System.Collections.Generic;

namespace ECS;
public interface IArchetype
{
    public Dictionary<ComponentType, Component> ArchetypeComponents { get; }
}
