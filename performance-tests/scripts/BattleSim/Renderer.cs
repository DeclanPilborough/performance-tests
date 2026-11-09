using Godot;
using System;
using System.Collections.Generic;

public partial class Renderer : Node2D
{
    [Export]
    public Texture2D aliveShipSprite;

    public MultiMesh aliveShipsMultiMesh = new();

    public override void _Ready()
    {
        base._Ready();

        AddChild(new MultiMeshInstance2D()
        {
            Texture = aliveShipSprite,
            Multimesh = aliveShipsMultiMesh
        });

        aliveShipsMultiMesh.InstanceCount = 50000;

        aliveShipsMultiMesh.Mesh = new QuadMesh()
        {
            Size = new(16f, 16f)
        };
    }

    public void Render(RenderingData data)
    {
        foreach (KeyValuePair<int, Vector2> kvp in data.liveShipPositions)
        {
            Transform2D transform = new(data.liveShipRotations[kvp.Key], Vector2.One,  0f, kvp.Value);
            aliveShipsMultiMesh.SetInstanceTransform2D(kvp.Key, transform);
        }

        foreach (KeyValuePair<int, Vector2> kvp in data.deadShipPositions)
        {
            Transform2D transform = new(0f, Vector2.Zero, 0f, kvp.Value);
            aliveShipsMultiMesh.SetInstanceTransform2D(kvp.Key, transform);
        }
    }

}
