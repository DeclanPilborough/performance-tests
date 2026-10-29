using Godot;
using System;
using ECS;
using System.Threading.Tasks;

public partial class SimRunner : Node2D
{
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
		// populate the world with variables
		world.PopulateWorld(/*variables*/);

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
