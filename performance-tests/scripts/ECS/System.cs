using Godot;
using System;

namespace ECS;
public abstract partial class System : RefCounted
{
    public abstract void Run();
}
