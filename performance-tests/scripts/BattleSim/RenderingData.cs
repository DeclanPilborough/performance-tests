using Godot;
using System;
using System.Collections.Generic;

public struct RenderingData
{
    public Dictionary<int, Vector2> liveShipPositions;
    public Dictionary<int, float> liveShipRotations;

    public Dictionary<int, Vector2> deadShipPositions;
}
