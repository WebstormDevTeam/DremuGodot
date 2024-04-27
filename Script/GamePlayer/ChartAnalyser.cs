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
    public List <int > p1;
    /// <summary>
    /// 
    /// </summary>
    public List <int > p2;
}

public class NoteItem
{
    /// <summary>
    /// 
    /// </summary>
    public List <int > time;
    /// <summary>
    /// 
    /// </summary>
    public int type;
    /// <summary>
    /// 
    /// </summary>
    public List <int > end;
}

public class LinesItem
{
    /// <summary>
    /// 
    /// </summary>
    public int num;
    /// <summary>
    /// 
    /// </summary>
    public List <LineItem > line;
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public List <int > pos;
    /// <summary>
    /// 
    /// </summary>
    public int rot;
    /// <summary>
    /// 
    /// </summary>
    public List <LinesItem > lines;
    /// <summary>
    /// 
    /// </summary>
    public List <MotionItem > motion;
}

public class ChartItem
{
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public string Name;
    /// <summary>
    /// 
    /// </summary>
    public string Hard;
    /// <summary>
    /// 
    /// </summary>
    public int DefaultBPM;
    /// <summary>
    /// 
    /// </summary>
    public List <ChartItem > Chart;
    /// <summary>
    /// 
    /// </summary>
    public List <ActionItem > Action;
}
