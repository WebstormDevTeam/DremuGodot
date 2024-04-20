using Godot;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DremuGodot.Script.GamePlayer.GuideLine;
//TODO:还有BUG，这个BUG已经改了三个星期了，人已经要疯了，又重写一下试试
public partial class Tap : Sprite2D
{
    public List<int> time = [1,0,1];

    public override void _Process(double delta)
    {
        GD.Print($"{time[2]}");
    }
}

