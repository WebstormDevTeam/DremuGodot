using Godot;
using System;
using System.Collections.Generic;
using DremuGodot.Script.GamePlayer.GuideLine;
using DremuGodot.Script.UniLib;
using Godot.Collections;
using Newtonsoft.Json;

/*                            _ooOoo_
 *                           o8888888o
 *                           88" . "88
 *                           (| -_- |)
 *                            O\ = /O
 *                        ____/`---'\____
 *                      .   ' \\| |// `.
 *                       / \\||| : |||// \
 *                     / _||||| -:- |||||- \
 *                       | | \\\ - /// | |
 *                     | \_| ''\---/'' | |
 *                      \ .-\__ `-` ___/-. /
 *                   ___`. .' /--.--\ `. . __
 *                ."" '< `.___\_<|>_/___.' >'"".
 *               | | : `- \`.;`\ _ /`;.`/ - ` : | |
 *                 \ \ `-. \_ __\ /__ _/ .-` / /
 *         ======`-.____`-.___\_____/___.-`____.-'======
 *                            `=---='
 *
 *         .............................................
 *                  佛祖镇楼                  BUG辟易
 *          佛曰:
 *                  写字楼里写字间，写字间里程序员；
 *                  程序人员写程序，又拿程序换酒钱。
 *                  酒醒只在网上坐，酒醉还来网下眠；
 *                  酒醉酒醒日复日，网上网下年复年。
 *                  但愿老死电脑间，不愿鞠躬老板前；
 *                  奔驰宝马贵者趣，公交自行程序员。
 *                  别人笑我忒疯癫，我笑自己命太贱；
 *                  不见满街漂亮妹，哪个归得程序员？
 *
 *                  南无日夜辛勤劳苦程序员菩萨
 * 
 *
 *
 * 
 */


namespace DremuGodot.Script.GamePlayer
{
	public partial class GameController : Node2D
	{
		public static float timecode;
		private int frames = 0;
		
		private List<List<Vector2>> point = new List<List<Vector2>>{new List<Vector2>{new Vector2(0,0),new Vector2(100,-1000),new Vector2(1000,-10),new Vector2(1000,-1000)}};
		public override async void _Ready()
		{
			Engine.MaxFps = 60;
			string chartPath = "res://Chart/TestJson.json";
			string jsonString = FileAccess.GetFileAsString(chartPath);
			Root ChartData = ChartAnalyser.GetChartData(jsonString);
			
			
			// line.ThisCurves = point;
			LineRenderer line = new LineRenderer();
			
			CoordinateController coordinateController = new CoordinateController();
			AddChild(coordinateController);
			//TODO:坐标系的代码还没有写
			// coordinateController.Init();
			// coordinateController.Move([100,100],1.0f);
			// coordinateController.Rotation(10, 1.0f);
			// coordinateController.Play();
			line.SetLineRenderer(point);
			List<INote> notes = new List<INote>
			{
				new TapController(line,[1,1,4])
			};
			coordinateController.AddChild((TapController)notes[0]);
			notes[0].init();
			coordinateController.AddChild(line);
			// GD.Print(ChartData.Chart.CoordinateSystems.Count);
		}

		private void setTimeCode(int f)
		{
			timecode = frames / Engine.MaxFps;
		}

		public override void _Process(double delta)
		{
			frames++;
		}
	}
}
