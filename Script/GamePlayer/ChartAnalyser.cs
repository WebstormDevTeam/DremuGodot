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

public class LineItem
{
    /// <summary>
    /// 引导线的起始位置
    /// </summary>
    public List <int > start;
    /// <summary>
    /// 引导线的结束位置
    /// </summary>
    public List <int > end;
    /// <summary>
    /// 引导线的控制点1
    /// </summary>
    public List <int > p1;
    /// <summary>
    /// 引导线的控制点2
    /// </summary>
    public List <int > p2;
}

public class NoteItem
{
    /// <summary>
    /// Note的时间位置
    /// </summary>
    public List <int > time;
    /// <summary>
    /// Note的类型
    /// </summary>
    public int type;
    /// <summary>
    /// 若为Hold，Note的结束时间
    /// </summary>
    public List <int > end;
}

public class LinesItem
{
    /// <summary>
    /// 引导线的编号
    /// </summary>
    public int num;
    /// <summary>
    /// 引导线
    /// </summary>
    public List <LineItem > line;
    /// <summary>
    /// 引导线上的Note
    /// </summary>
    public List <NoteItem > note;
}

public class MotionItem
{
    /// <summary>
    /// 
    /// </summary>
    public string type;
    /// <summary>
    /// 
    /// </summary>
    public List <int > begin;
    /// <summary>
    /// 
    /// </summary>
    public List <int > over;
    /// <summary>
    /// 
    /// </summary>
    public List <int > start;
    /// <summary>
    /// 
    /// </summary>
    public List <int > end;
    /// <summary>
    /// 
    /// </summary>
    public string curve;
}

public class Coordinate
{
    /// <summary>
    /// 坐标系位置
    /// </summary>
    public List <int > pos;
    /// <summary>
    /// 坐标系旋转
    /// </summary>
    public int rot;
    /// <summary>
    /// 坐标系上的引导线集
    /// </summary>
    public List <LinesItem > lines;
    /// <summary>
    /// 坐标系变换
    /// </summary>
    public List <MotionItem > motion;
}

public class ChartItem
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public Coordinate coordinate;
}

public class Pos
{
    /// <summary>
    /// 
    /// </summary>
    public List <int > start;
    /// <summary>
    /// 
    /// </summary>
    public List <int > end;
    /// <summary>
    /// 
    /// </summary>
    public string curve;
}

public class ActionItem
{
    /// <summary>
    /// 
    /// </summary>
    public string type;
    /// <summary>
    /// 
    /// </summary>
    public string text;
    /// <summary>
    /// 
    /// </summary>
    public List <int > begin;
    /// <summary>
    /// 
    /// </summary>
    public List <int > over;
    /// <summary>
    /// 
    /// </summary>
    public Pos pos;
}

public class Root
{
    /// <summary>
    /// 谱面名称
    /// </summary>
    public string Name;
    /// <summary>
    /// 谱面难度
    /// </summary>
    public string Hard;
    /// <summary>
    /// 谱面的默认BPM
    /// </summary>
    public int DefaultBPM;
    /// <summary>
    /// 谱面数据
    /// </summary>
    public List <ChartItem > Chart;
    /// <summary>
    /// 我不知道这是啥
    /// </summary>
    public List <ActionItem > Action;
}
