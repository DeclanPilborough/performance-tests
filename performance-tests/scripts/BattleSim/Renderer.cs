using Godot;
using System;
using System.Collections.Generic;

public partial class Renderer : Node2D
{
    public Texture2D aliveShipSprite;

    public MultiMesh aliveShipsMultiMesh;

    public override void _Ready()
    {
        base._Ready();

        AddChild(new MultiMeshInstance2D()
        {
            Texture = aliveShipSprite,
            Multimesh = aliveShipsMultiMesh = new()
        });
    }

    public void Render(RenderingData data)
    {
        aliveShipsMultiMesh.InstanceCount = data.liveShipPositions.Count;

        foreach (KeyValuePair<int, Vector2> kvp in data.liveShipPositions)
        {
            Transform2D transform = new(data.liveShipRotations[kvp.Key], kvp.Value);
            aliveShipsMultiMesh.SetInstanceTransform2D(kvp.Key, transform);
        }
    }

}
