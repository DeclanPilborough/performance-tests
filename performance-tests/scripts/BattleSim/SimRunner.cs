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

	public void StartSim(int numberOfShips)
	{
		running = true;

		renderer.SetInstanceCount(numberOfShips);

		// fake it till you make it	
		int shipCount = numberOfShips;

		int rowCount = 245;
		int spacing = 20;

		List<Dictionary<ComponentType, Component>> worldPopulation = new(0);
		
		for (int i = 0; i < shipCount; i++)
		{
			int rows = i / rowCount;
			int col = rows * spacing;
			float xPos = 10 + col;
			float yPos = 10 + ((i % rowCount) + 1) * spacing;
			
			int team = i >= shipCount/2 ? 1 : 0;

			if (team == 1)
			{
				xPos += 1000;
			}

			worldPopulation.Add(new Dictionary<ComponentType, Component>(BasicShipArchetype.ArchetypeComponents)
			{
				[ComponentType.Position] = new PositionComponent()
				{
					Position = new(xPos, yPos)
				},
				[ComponentType.Team] = new TeamComponent()
				{
					Team = team
				}
			});
		}

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
