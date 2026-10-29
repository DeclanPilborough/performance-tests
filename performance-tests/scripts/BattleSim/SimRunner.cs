using Godot;
using System;
using ECS;
using System.Threading.Tasks;
using System.Collections.Generic;

public partial class SimRunner : Node2D
{
	[Export]
	public Renderer renderer;

	World world;

	Task simRunTask;

	bool running;

	RenderingData renderingData = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		world = new();
	}

	public void StartSim(/*variables*/)
	{
		running = true;

		// fake it till you make it
		Dictionary<int, IArchetype> worldPopulation = new()
		{
			{100, new BasicShipArchetype()}
		};

		// populate the world with variables
		world.PopulateWorld(worldPopulation);

		renderingData = world.GetRenderingData();

		simRunTask = Task.Run(world.RunWorldFrame);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (running)
		{
			if (simRunTask.Status == TaskStatus.RanToCompletion) // need to implement checks for failed, cancelled, etc.
			{
				renderingData = world.GetRenderingData();

				simRunTask = Task.Run(world.RunWorldFrame);
			}
		}

		// rendering using data
		// basics are position and rotation
		// eventually, shooting and stuff

		renderer.Render(renderingData);
	}
}
