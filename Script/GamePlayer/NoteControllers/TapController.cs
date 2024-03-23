using Godot;
using System;
using System.Collections.Generic;
using DremuGodot.Script.GamePlayer;

public partial class TapController : Node,INote
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Create(int number, List<int> BeatCode)
	{
		GD.Print("TapIsCreated");
	}

	public void JudgeAndDel(double TouchTime)
	{
		GD.Print("Touch");
	}
}
