using System.Collections.Generic;
using System.Linq;
using DremuGodot.Script.UniLib;
using Godot;

namespace DremuGodot.Script.GamePlayer.GuideLine;

public partial class LineRenderer : Line2D
{
	// 存储点的数组
	private Vector3[] _points;
	// 点队列
	public Queue<Vector2> PointsQueue = new Queue<Vector2>();
	// 点的个数
	public int pointNumber = 100;
	// 移动速度
	public double speed;
	// 上一个点
	public Vector2 LastPoint;
	// 默认速度
	private double DefaultSpeed = -4;
	// 计数器
	private int n = 0;
	// 当前曲线组成的列表
	public List<List<Vector2>> ThisCurves;
	//已经下落的像素数
	public int PixelCount = 0;

	// 泛型方法，用于实例化 LineRenderer 对象
	public static T newLineRenderer<T>(PackedScene lineRendererPackedScene) where T : class
	{
		return lineRendererPackedScene.Instantiate() as T;
	}
	/// <summary>
	/// 创建引导线（由多个三阶贝赛尔曲线构成）
	/// </summary>
	/// <param name="Curves">由两个List嵌套构成一个是曲线的List，一个是曲线的四个控制点</param>
	///
	// 设置引导线
	public void SetLineRenderer(List<List<Vector2>> Curves)
	{
		// 设置速度
		speed = (speed > 0 && speed != null ? speed : DefaultSpeed);
		// 遍历曲线列表，加载点到队列中
		foreach (List<Vector2> curve in Curves)
		{
			AddPoints(curve[0], curve[1], curve[2], curve[3]);
		}
		// GD.Print(PointsQueue.Count());
	}


	/// <summary>
	/// 将点加载到队列中，只可以调用一次，以节省内存空间
	/// 使用贝赛尔曲线的方式进行绘制
	/// </summary>
	/// <param name="p0">起始端点</param>
	/// <param name="p1">控制点1</param>
	/// <param name="p2">控制点2</param>
	/// <param name="p3">结束端点</param>
	// 添加点到队列中
	public void AddPoints(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		// 循环生成点，并添加到队列中
		for (float i = 0; i <= 1; i += 0.01f)
		{
			Vector2 point = Bezier.BezierCurve(p0, p1, p2, p3, i);
			// point.Y = -point.Y;
			PointsQueue.Enqueue(point);
		}
	}

	/// <summary>
	/// 将线渲染出来
	/// </summary>
	/// <param name="renderPointsNumber"></param>
	// 渲染点并更新位置
	public void RenderPoints()
	{
		// 当计数大于渲染点数时，取点数；小于时，取当前计数
		List<Vector2> points = PointsQueue.ToList()
			.GetRange(0, PointsQueue.Count >= pointNumber ? pointNumber : PointsQueue.Count);
		for (int i = 0; i < (PointsQueue.Count >= pointNumber ? pointNumber : PointsQueue.Count); i++)
		{
			float y = points[i].Y;
			// n*delta以调整下落速度
			points[i] = new Vector2(points[i].X, y - (float)(n * speed)); //TODO:屎山代码，待大佬修改
			
			if (points[i].Y > 0)
			{
				LastPoint = PointsQueue.Dequeue();
				PixelCount++;
			}
		}

		Points = points.ToArray();
		
		n++;
	}

	// 在 _Ready 函数中设置折线的属性
	public override void _Ready()
	{
		// 设置折线的属性
		Width = 10f; // 宽度为 10 像素
		DefaultColor = new Color(1f, 0f, 0f); // 颜色为红色
		TextureMode = LineTextureMode.Tile; // 纹理模式为平铺
		// line.Texture = GD.Load<Texture>("res://line_texture.png"); // 加载纹理资源
		Points = new Vector2[100];

		/*AddPoints(new Vector2(0, 0), new Vector2(100, -1000), new Vector2(1000, -10), new Vector2(1000, -1000));*/
	}

	// 在 _Process 函数中更新折线的点
	public override void _Process(double delta)
	{
		// GD.Print(GlobalPosition);
		RenderPoints();
	}
}
