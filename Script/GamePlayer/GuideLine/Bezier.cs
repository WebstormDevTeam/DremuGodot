using Godot;

namespace DremuGodot.Script.GamePlayer.GuideLine
{
    public class Bezier
    {
        /// <summary>
        /// 贝赛尔曲线生成（三阶）
        /// </summary>
        /// <param name="p0">起始端点</param>
        /// <param name="p1">控制点1</param>
        /// <param name="p2">控制点2</param>
        /// <param name="p3">结束端点</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector2 BezierCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            // 计算系数
            float a = (1 - t) * (1 - t) * (1 - t);
            float b = 3 * t * (1 - t) * (1 - t);
            float c = 3 * t * t * (1 - t);
            float d = t * t * t;

            // 计算点的坐标
            float x = a * p0.X + b * p1.X + c * p2.X + d * p3.X;
            float y = a * p0.Y + b * p1.Y + c * p2.Y + d * p3.Y;
        

            // 返回点
            return new Vector2(x,y);
        }
    }
}

