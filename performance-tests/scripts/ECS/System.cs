using Godot;
using System;

namespace ECS;
public abstract partial class System : RefCounted
{
    public abstract ComponentType ComponentFilter { get; }
    public abstract void Run();
}
