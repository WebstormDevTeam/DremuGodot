using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using DremuGodot.Script.GamePlayer.GuideLine;
using DremuGodot.Script.UniLib;
using Godot.Collections;
using Newtonsoft.Json;
using Transform2D = Godot.Transform2D;

namespace DremuGodot.Script.GamePlayer
{
    // 游戏控制器类
    public partial class GameController : Node2D
    {
        // 导出场景属性
        [Export] public PackedScene lineRenderer;
        [Export] public PackedScene tap;
        [Export] public PackedScene drag;
        [Export] public PackedScene flick;
        //[Export] public int DefaultFPS = 60;

        [Export] public Label NameLabel;

        // 是否自动播放属性
        [Export] public bool isAutoPlay;

		// 静态时间码和实例
		public static float timecode;
		public static GameController Instance;
		// 谱面数据
		public Root ChartData;
		
		public List<LineRenderer> GuideLines = new List<LineRenderer>();
		public List<CoordinateController> Coordinates = new List<CoordinateController>();
		public List<Tap> Taps = new List<Tap>();
		public List<Drag> Drags = new List<Drag>();
		public List<Flick> Flicks = new List<Flick>();
		public Hashtable ActionHashTable = new Hashtable();
		// 当前帧计数
		public int Frames = 0;
		public float NowTimeCode;
		public int TapCount = 0;
		
		// 构造函数，实例化静态实例
		public GameController()
		{
			Instance = this;
		}

        // 点的列表!测试用的
        private List<List<Vector2>> point = new List<List<Vector2>>
        {
            new List<Vector2>
                { new Vector2(0, 0), new Vector2(100, -1000), new Vector2(1000, -10), new Vector2(1000, -1000) }
        };


		// _Ready方法，当节点准备好时调用
		public override void _Ready()
		{
			//设置AutoPlay的默认值
			if (isAutoPlay is not true)
				isAutoPlay = true;
			
			// 设置最大帧率
			Engine.MaxFps = 60;
			//读取和解析Json文件
			string chartPath = "res://Chart/TestJson.json";
			string jsonString = FileAccess.GetFileAsString(chartPath);
			ChartData = ChartAnalyser.GetChartData(jsonString);
			//设置标题和难度
			NameLabel.Text = ChartData.Name + " " + ChartData.Hard;
			createElement();
			
			//Debug Code
			// 创建新的线渲染器并添加到节点
			GuideLines.Add(LineRenderer.newLineRenderer<LineRenderer>(lineRenderer));
			GuideLines[0].SetLineRenderer(point);
			AddChild(GuideLines[0]);
			// 创建新的Tap并添加到节点
			Tap _tap = Tap.newNote<Tap>(tap);
			_tap.Visible = true; //设置可见性
			AddChild(_tap);
			_tap.InitNote(GuideLines[0], new List<int> { 0, 1, 2 });
			
			_tap.Connect("DestroyTap", new Callable(this, nameof(OnDestroyTap))); //连接摧毁Tap信号
			
			//
			// // 创建新的Drag并添加到节点
			// Drag _drag = Drag.newNote<Drag>(drag);
			// _drag.Visible = true; //设置可见性
			// AddChild(_drag);
			// _drag.InitNote(GuideLines[0], [1, 1, 4]);
			//
			// _drag.Connect("DestroyDrag", new Callable(this, nameof(OnDestroyDrag))); //连接摧毁Drag信号
			//
			//
			// // 创建新的Flick并添加到节点
			// Flick _flick = Flick.newNote<Flick>(flick);
			// _flick.Visible = true; //设置可见性
			// AddChild(_flick);
			// _flick.InitNote(GuideLines[0], [1, 2, 4]);
			//
			// _flick.Connect("DestroyFlick", new Callable(this, nameof(OnDestroyFlick))); //连接摧毁Flick信号
		}

        public void createElement()
        {
            foreach (ChartItem Coordinate in ChartData.Chart)
            {
                Coordinates.Add(CoordinateController.newCoordinate());
                
                foreach (LinesItem lineGroup in Coordinate.coordinate.lines)
                {
                    GuideLines.Add(LineRenderer.newLineRenderer<LineRenderer>(lineRenderer));
                    
                    foreach (var noteGroup in lineGroup.note)
                    {
                        ActionHashTable.Add(TimecodeTras.FromBpmcodeToTimecode(noteGroup.time, 60), noteGroup.type);//debug:bpm=60
                        // 创建Note,0为Tap,1为Drag,2为Flick,3为Hold
                        if (noteGroup.type == 0)
                        {
                            Taps.Add(Tap.newNote<Tap>(tap));
                        }
                        else if (noteGroup.type == 1)
                        {
                            Drags.Add(Drag.newNote<Drag>(drag));
                        }
                        else if (noteGroup.type == 2)
                        {
                            Flicks.Add(Flick.newNote<Flick>(flick));
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 创建Note
        /// </summary>
        /// <param name="nowTimeCode">当前的时间码</param>
        /// <param name="item">Note数据</param>
        public void CreateNote(float nowTimeCode,int type)
        {
            if (type== 0)
            {
                //TODO:Tap的创建
                Taps[TapCount].Visible = true; //设置可见性
                GD.Print(nowTimeCode);
                Taps[TapCount].InitNote(GuideLines[0], nowTimeCode);
                Taps[TapCount].Connect("DestroyTap", new Callable(this, nameof(OnDestroyTap))); //连接摧毁Tap信号
                AddChild(Taps[TapCount]);
                TapCount++;
            }
            
        }
        /// <summary>
        /// 用于信号连接的函数
        /// 主要用来处理打击特效和判定
        /// </summary>
        // 处理销毁Tap信号
        public void OnDestroyTap()
        {
            GD.Print($"Connected");
        }

        // 处理销毁Drag信号
        public void OnDestroyDrag()
        {
            GD.Print($"Connected");
        }

        // 处理销毁Flick信号
        public void OnDestroyFlick()
        {
            GD.Print($"Connected");
        }

		// 设置时间码
		private void setTimeCode(int f)
		{
			timecode = Frames / Engine.MaxFps;
		}

		// 处理每帧的逻辑更新
		public override void _Process(double delta)
		{
			Frames++;
			//计算当前的时间码
			NowTimeCode = (float)Math.Round((float)Frames / 60, 2);//debug:fps=60
			if (ActionHashTable[NowTimeCode] != null)
			{
				CreateNote(NowTimeCode,(int)ActionHashTable[NowTimeCode]);
			}

            
            // GD.Print(frames);
            //  GD.Print(NowTimeCode);
            // GD.Print($"");

            // tap.update();
        }

        // 物理逻辑更新
        public override void _PhysicsProcess(double delta)
        {
        }
    }
}
