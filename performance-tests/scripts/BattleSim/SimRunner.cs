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
		world.Start();
	}

	public void StartSim(/*variables*/)
	{
		running = true;

		// fake it till you make it
		Dictionary<int, IArchetype> worldPopulation = new()
		{
			{1, new BasicShipArchetype()}
		};

		// populate the world with variables
		world.PopulateWorld(worldPopulation);

		renderingData = world.GetRenderingData();

		simRunTask = Task.Run(() => world.RunWorldFrame(0));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (running)
		{
			if (simRunTask.Status == TaskStatus.RanToCompletion || simRunTask.Status == TaskStatus.Canceled || simRunTask.Status == TaskStatus.Faulted)
			{
				renderingData = world.GetRenderingData();

				simRunTask = Task.Run(() => world.RunWorldFrame(delta));
			}

			// rendering using data
			// basics are position and rotation
			// eventually, shooting and stuff

			renderer.Render(renderingData);
		}
	}
}
