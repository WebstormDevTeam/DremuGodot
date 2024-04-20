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
    public delegate void DestroyTapEventHandler();
    
    private Tween _tween;
    private Vector2 _endPosition;
    private double _t = 0.1f;
    
    public static T newTap<T>(PackedScene tapPackedScene) where T:class
    {
        return tapPackedScene.Instantiate() as T;
    }

    public void InitTap(LineRenderer lineRenderer,List<int> timeCode)
    {
        var pointsQueue = lineRenderer.PointsQueue.ToList();
		
        float judgetime = TimecodeTras.FromBpm(timeCode,60);
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
        if (Position.Y <= 90)
        {
            int fallSpeed = 4;
            var newPosition = Position;
            newPosition.Y += fallSpeed;
            Position = newPosition;
            GD.Print($"{Position}");
            if (Position.Y>=0 && Position.Y<=3)
            {
                EmitSignal(SignalName.DestroyTap);
                GD.Print($"Destory");
            }
        }
    }
}

