using Godot;
using System;

namespace ECS;
public partial class TargetingComponent : Component
{
    // targeting variables - vision range, shape, etc.

    public float TargetPingRechargeTime = 0.0f;
    public float PingRecharge = 0.0f;
}
