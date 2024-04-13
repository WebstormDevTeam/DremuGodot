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
	public Tween tween;

	private float dt;
	public TapController()
	{
		
	}

	public TapController(LineRenderer lineRenderer, List<int> time)
	{
		pointsQueue = lineRenderer.PointsQueue.ToList();
		
		float judgetime = TimecodeTras.FromBpm(time,60);
		count = TimecodeTras.ToFps(judgetime, 60);
		dt = judgetime - GameController.timecode;//计算下落时间 && 在这里不使用微积分进行动态处理
		
		Position = pointsQueue[count]; //设置初始位置
		
		GD.Print(dt);
	}

	public void init()
	{
		tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", new Vector2(pointsQueue[count].X,0), dt);
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
		GD.Print(Position);
	}

	
}
