using Godot;
using System;
using System.Threading.Tasks;
using DremuGodot.Script.GamePlayer;

public partial class CoordinateController : Node2D
{
    //TODO:重写坐标系的代码
    public static CoordinateController newCoordinate()
    {
        return new CoordinateController();
    }
}
