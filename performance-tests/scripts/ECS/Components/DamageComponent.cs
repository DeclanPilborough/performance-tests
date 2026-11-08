using Godot;
using System;

namespace ECS;
public partial class DamageComponent : Component
{
    // damage variables, however those should be done
    public float DamagePerShot = 0.0f;

    public float ShotRechargeTime = 0.0f;

    public float RechargeTimer = 0.0f;
}
