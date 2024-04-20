using Godot;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DremuGodot.Script.GamePlayer;
using DremuGodot.Script.GamePlayer.GuideLine;
using DremuGodot.Script.UniLib;

//TODO:还有BUG，这个BUG已经改了三个星期了，人已经要疯了，又重写一下试试
public partial class Tap : Sprite2D
{
    
    private Tween _tween;
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
        
    }
    
    public override void _Process(double delta)
    {
        
    }
}

