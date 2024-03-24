using Godot;
using System;
using System.Threading.Tasks;
using DremuGodot.Script.GamePlayer;

public partial class CoordinateController : Node2D
{

	public Tween tween;

	public void Init()
	{
		tween = GetTree().CreateTween();
	}

	public void Move(int[] pos, float time)
	{
		tween.TweenProperty(this, "position", new Vector2(pos[0],pos[1]), time);
		tween.Play();
		// await ToSignal(tween, "tween_all_completed");
	}

	public void Rotation(float rot, float time)
	{
		tween.TweenProperty(this, "rotation", rot, time);
		tween.Play();
		// await ToSignal(tween, "tween_all_completed");
	}
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
