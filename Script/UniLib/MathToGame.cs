using Godot;

namespace DremuGodot.Script.UniLib;

public class MathToGame
{
    public static Vector2 GetCoordinate(Vector2 GameCoordinate)
    {
        return new Vector2(GameCoordinate.X, -GameCoordinate.Y);
        
        
    }
}