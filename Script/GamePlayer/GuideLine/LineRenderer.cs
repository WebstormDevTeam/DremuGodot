using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DremuGodot.Script.GamePlayer.GuideLine;

public class LineRenderer : Node
{
    
    private Vector3[] _points;
    public Queue<Vector2> PointsQueue = new Queue<Vector2>();
    public int pointNumber = 100;
    public static double speed;
    private double DefaultSpeed=1;
    private int n=0;
    
    // 创建 Line2D 节点
    Line2D line = new Line2D();
    
    
    /// <summary>
    /// 将点加载到队列中，只可以调用一次，以节省内存空间
    /// 使用贝赛尔曲线的方式进行绘制
    /// </summary>
    /// <param name="p0">起始端点</param>
    /// <param name="p1">控制点1</param>
    /// <param name="p2">控制点2</param>
    /// <param name="p3">结束端点</param>
    public void AddPoints(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {

        // 循环生成点，并添加到队列中
        for (float i = 0; i <= 1; i+=0.01f)
        {
            Vector2 point = Bezier.BezierCurve(p0,p1,p2,p3,i);
            PointsQueue.Enqueue(point);
        }
    }
    
    /// <summary>
    /// 将线渲染出来
    /// </summary>
    /// <param name="renderPointsNumber"></param>
    public void RenderPoints()
    {
        //当计数大于渲染点数时，取点数；小于时，取当前计数
        List<Vector2> points = PointsQueue.ToList().GetRange(0,PointsQueue.Count >= pointNumber ? pointNumber : PointsQueue.Count);
        // Debug.Log(PointsQueue.Count);
        for (int i = 0; i < (PointsQueue.Count >= pointNumber ? pointNumber : PointsQueue.Count); i++)
        {
            // Debug.Log(PointsQueue.Dequeue());
            float y = points[i].Y;
            //n*delta以调整下落速度
            points[i] = new Vector2(points[i].X, y - (float)(n*(speed>0&&speed!=null?speed:DefaultSpeed)));
            // Debug.Log(points[i]);
            //TODO:当y==0时，将坐标传出到判定点上
            if (points[i].Y<0)
            {
                PointsQueue.Dequeue();
            }
                
            // RenderQueue.Enqueue(points[i]);
        }

        line.Points = points.ToArray();
        n++;
    }
    

    public override void _Ready()
    {
        // 设置折线的属性
        line.Width = 10f; // 宽度为 10 像素
        line.DefaultColor = new Color(1f, 0f, 0f); // 颜色为红色
        line.TextureMode = Line2D.LineTextureMode.Tile; // 纹理模式为平铺
        // line.Texture = GD.Load<Texture>("res://line_texture.png"); // 加载纹理资源
        line.Points = new Vector2[100];
        // 添加折线的点
        line.SetPointPosition(0,new Vector2(100f, 100f)); // 第一个点
        line.SetPointPosition(1,new Vector2(200f, 150f)); // 第二个点
        line.SetPointPosition(2,new Vector2(300f, 100f)); // 第三个点

        // 将 Line2D 节点添加到场景中
        AddChild(line);
    }

    // 在 _Process 函数中更新折线的点
    public override void _Process(double delta)
    {

    }
}