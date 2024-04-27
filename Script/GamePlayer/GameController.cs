using Godot;
using System;
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

        [Export] public Label NameLabel;

        // 是否自动播放属性
        [Export] public bool isAutoPlay;

        // 静态时间码和实例
        public static float timecode;
        public static GameController Instance;
        public Root ChartData;
        public List<LineRenderer> GuideLines = new List<LineRenderer>();
        public List<Tap> Taps = new List<Tap>();
        public List<Drag> Drags = new List<Drag>();
        public List<Flick> Flicks = new List<Flick>();
        private int frames = 0;

        // 构造函数，实例化静态实例
        public GameController()
        {
            Instance = this;
        }

        // 点的列表
        private List<List<Vector2>> point = new List<List<Vector2>>
        {
            new List<Vector2>
                { new Vector2(0, 0), new Vector2(100, -1000), new Vector2(1000, -10), new Vector2(1000, -1000) }
        };

        // _Ready方法，当节点准备好时调用
        public override void _Ready()
        {
            if (isAutoPlay == null)
                isAutoPlay = true;

            // 设置最大帧率
            Engine.MaxFps = 60;
            string chartPath = "res://Chart/TestJson.json";
            string jsonString = FileAccess.GetFileAsString(chartPath);
            ChartData = ChartAnalyser.GetChartData(jsonString);
            //设置标题和难度
            NameLabel.Text = ChartData.Name + " " + ChartData.Hard;
            ChartPlay();
            //Debug Code

            // 创建新的线渲染器并添加到节点
            // LineRenderer line = LineRenderer.newLineRenderer<LineRenderer>(lineRenderer);
            // line.SetLineRenderer(point);
            // AddChild(line);
            // // 创建新的Tap并添加到节点
            // Tap _tap = Tap.newNote<Tap>(tap);
            // _tap.Visible = true; //设置可见性
            // AddChild(_tap);
            // _tap.InitNote(line, [1, 0, 4]);
            //
            // _tap.Connect("DestroyTap", new Callable(this, nameof(OnDestroyTap))); //连接摧毁Tap信号
            //
            //
            // // 创建新的Drag并添加到节点
            // Drag _drag = Drag.newNote<Drag>(drag);
            // _drag.Visible = true; //设置可见性
            // AddChild(_drag);
            // _drag.InitNote(line, [1, 1, 4]);
            //
            // _drag.Connect("DestroyDrag", new Callable(this, nameof(OnDestroyDrag))); //连接摧毁Drag信号
            //
            //
            // // 创建新的Flick并添加到节点
            // Flick _flick = Flick.newNote<Flick>(flick);
            // _flick.Visible = true; //设置可见性
            // AddChild(_flick);
            // _flick.InitNote(line, [1, 2, 4]);
            //
            // _flick.Connect("DestroyFlick", new Callable(this, nameof(OnDestroyFlick))); //连接摧毁Flick信号
        }

        public void ChartPlay()
        {
            foreach (ChartItem Coordinate in ChartData.Chart)
            {
                foreach (LinesItem lineGroup in Coordinate.coordinate.lines)
                {
                    List<List<Vector2>> point = new List<List<Vector2>>();
                    
                    for (int i = 0; i < lineGroup.line.Count; i++)
                    {
                        // 因为Godot使用的坐标系和一般的坐标系相反，所以需要反向坐标轴
                        point = new List<List<Vector2>>
                        {
                            new List<Vector2>
                            {
                                new(lineGroup.line[i].start[0], -lineGroup.line[i].start[1]),
                                new(lineGroup.line[i].p1[0], -lineGroup.line[i].p1[1]),
                                new(lineGroup.line[i].p2[0], -lineGroup.line[i].p2[1]),
                                new(lineGroup.line[i].end[0], -lineGroup.line[i].end[1])
                            }
                        };
                        
                    }
                    GuideLines.Add(LineRenderer.newLineRenderer<LineRenderer>(this.lineRenderer)); // 创建新的线渲染器并添加到节点
                    GuideLines[lineGroup.num].SetLineRenderer(point);
                    AddChild(GuideLines[lineGroup.num]);
                    
                }
            }
        }

        // 创建Note
        public void CreateNote()
        {
            
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
            timecode = frames / Engine.MaxFps;
        }

        // 处理每帧的逻辑更新
        public override void _Process(double delta)
        {
            frames++;


            // tap.update();
        }

        // 物理逻辑更新
        public override void _PhysicsProcess(double delta)
        {
        }
    }
}