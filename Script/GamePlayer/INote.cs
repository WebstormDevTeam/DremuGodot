using System.Collections.Generic;
using DremuGodot.Script.GamePlayer.GuideLine;

namespace DremuGodot.Script.GamePlayer;

/// <summary>
/// 
/// </summary>
public interface INote
{
    void init();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TouchTime"></param>
    void JudgeAndDel(double TouchTime);
}