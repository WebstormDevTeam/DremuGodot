using Godot;
using System;
using System.Collections.Generic;
using DremuGodot.Script.GamePlayer;
using DremuGodot.Script.GamePlayer.GuideLine;

public partial class DragController : Node,INote
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void Create(int number, LineRenderer lineRenderer, List<int> time)
	{
		throw new NotImplementedException();
	}

	public void JudgeAndDel(double TouchTime)
	{
		throw new NotImplementedException();
	}
}
