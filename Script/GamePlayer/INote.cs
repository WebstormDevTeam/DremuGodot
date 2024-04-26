using System.Collections.Generic;
using DremuGodot.Script.GamePlayer.GuideLine;
using Godot;

namespace DremuGodot.Script.GamePlayer;

/// <summary>
/// 
/// </summary>
public interface INote
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lineRenderer"></param>
    /// <param name="timecode"></param>
    void InitNote(LineRenderer lineRenderer, List<int> timecode);

}