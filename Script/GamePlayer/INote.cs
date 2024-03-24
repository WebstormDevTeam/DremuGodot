using System.Collections.Generic;

namespace DremuGodot.Script.GamePlayer;

/// <summary>
/// 
/// </summary>
public interface INote
{
    /// <summary>
    /// 创建Note
    /// </summary>
    /// <param name="number">Note的编号</param>
    /// <param name="BeatCode">节拍码</param>
    void Create(int number,List<int> BeatCode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TouchTime"></param>
    void JudgeAndDel(double TouchTime);
}