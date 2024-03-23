using System.Collections.Generic;
using Newtonsoft.Json;

namespace DremuGodot.Script.GamePlayer;

public class ChartAnalyser
{
    /// <summary>
    /// 获取谱面数据
    /// </summary>
    /// <param name="json">传入一个字符串，字符串是JSON格式的谱面文件</param>
    /// <returns>谱面数据，c＃对象格式</returns>
    public static Root GetChartData(string json)
    {
        string jsonString = json;
        Root obj = JsonConvert.DeserializeObject<Root>(jsonString);
        return obj;
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Action
{
    public string ActionName;
    public List<List<int>> Time;
    public List<int> Rot;
    public List<List<int>> Pos;
}

public class Chart
{
    public List<CoordinateSystem> CoordinateSystems;
}

public class CoordinateSystem
{
    public string Num;
    public List<int> Pos;
    public int Rot;
    public List<SubCoordinateSystem> SubCoordinateSystems;
    public List<GuideLines> GuideLines;
    public List<Action> Actions;
}

public class GuideLines
{
    public int Num;
    public bool JudgementPointIsShow;
    public List<Line> lines;
}

public class Line
{
    public int Num;
    public List<int> Time;
    public string LineType;
    public List<int> Start;
    public List<int> End;
    public List<int> p1;
    public List<int> p2;
}

public class Root
{
    public string Name;
    public int Version;
    public string Hard;
    public int bpm;
    public Chart Chart;
}

public class SubCoordinateSystem
{
    public string Num;
    public List<int> Pos;
    public int Rot;
    public List<object> SubCoordinateSystems;
    public List<GuideLines> GuideLines;
    public List<Action> Actions;
}

