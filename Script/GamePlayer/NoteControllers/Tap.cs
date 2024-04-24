using System;
using Godot;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DremuGodot.Script.GamePlayer;
using DremuGodot.Script.GamePlayer.GuideLine;
using DremuGodot.Script.UniLib;

public partial class Tap : Sprite2D
{
	[Signal]
	public delegate void DestroyTapEventHandler(); //定义信号，使用委托类型

	private Tween _tween;
	private Vector2 _endPosition;
	private double _t = 0.1f;
	/// <summary>
	/// 实例化Tap，这里还要使用接口来实现（目前还没有做）
	/// </summary>
	/// <param name="notePackedScene">Export的TapScene，在这里传入</param>
	/// <typeparam name="T">泛型，传入的是Note的预制件类型</typeparam>
	/// <returns>实例化后的Note组件</returns>
	public static T newNote<T>(PackedScene notePackedScene) where T : class
	{
		return notePackedScene.Instantiate() as T;
	}
	/// <summary>
	/// 初始化Tap，只可以在代码中调用少量的次数，使用多了会出现效率问题
	/// </summary>
	/// <param name="lineRenderer">绑定的线渲染器</param>
	/// <param name="timeCode">判定的时间，不是创建的时间</param>
	public void InitTap(LineRenderer lineRenderer, List<int> timeCode)
	{
		// GD.Print("Tap is created");
		// 从点队列中获取对应时间的点
		var pointsQueue = lineRenderer.PointsQueue.ToList();
		// 时间码的转化
		float judgetime = TimecodeTras.FromBpm(timeCode, 60);
		var count = TimecodeTras.ToFps(judgetime, 60);

		Position = pointsQueue[count]; //设置初始位置
									   //设置结束的位置
		_endPosition.X = Position.X;
		_endPosition.Y = 0;

	}

	// public override void _PhysicsProcess(double delta)
	// {
	//     _t += Interpolation.Liner(0.1,Math.Round(delta,2));
	//
	//     Position = Position.Lerp(_endPosition, (float)Math.Round(_t,2));
	//     GD.Print($"{_t}");
	// }

	public override void _Process(double delta)
	{
		// 根据下落的位置进行判断，若小于90，则一直下落
		if (Position.Y <= 90)
		{
			int fallSpeed = 4;
			var newPosition = Position;
			newPosition.Y += fallSpeed; //注意！Y和数学的坐标系的方向是相反的
			Position = newPosition;
			// GD.Print($"{Position}");
			//在[0,3]这个中则视为判定了，但是实际上应当使用时间来进行判定
			if (Position.Y >= 0 && Position.Y <= 3 && GameController.Instance.isAutoPlay)
			{
				EmitSignal(SignalName.DestroyTap);//发送信号
				Visible = false;
				QueueFree();
				GD.Print($"Destory");
			}
			else if (Position.Y >= 30)
			{
				//处理不是AutoPlay的情况
				Visible = false;
				QueueFree();
				GD.Print($"Miss");
			}
		}
	}
}

