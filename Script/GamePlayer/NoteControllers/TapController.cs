using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using DremuGodot.Script.GamePlayer;
using DremuGodot.Script.GamePlayer.GuideLine;
using DremuGodot.Script.UniLib;

public partial class TapController : Node2D, INote
{

	public int count;
	public List<Vector2> pointsQueue;

	public TapController()
	{
		
	}

	public TapController(LineRenderer lineRenderer, List<int> time)
	{
		pointsQueue = lineRenderer.PointsQueue.ToList();
		float timecode = TimecodeTras.FromBpm(time,60);
		count = TimecodeTras.ToFps(timecode, 60);
	}
	
	
	

	public void JudgeAndDel(double TouchTime)
	{
		GD.Print("Touch");
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		count--;
		Position = pointsQueue[count];
	}

	
}
