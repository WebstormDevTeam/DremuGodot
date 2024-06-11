using System;
using System.Collections.Generic;

namespace DremuGodot.Script.UniLib;

public class TimecodeTras
{
    public static float FromBpm(List<int> BpmCode,int bpm)
    {
        return (float)Math.Round(((60f / bpm) * (BpmCode[0] + BpmCode[1] / (float)BpmCode[2])),2);
    }

    public static float FromFps(List<int> FpsCode, int fps)
    {
        // Convert the FpsCode[2] to float to ensure the division results in a float
        return FpsCode[0] * 60 + FpsCode[1] + (FpsCode[2] / (float)fps);
    }

    public static int ToFps(float timecode, int fps)
    {
        int totalFrames = (int)(timecode * fps);
        int seconds = (totalFrames % (fps * 60)) / fps;
        int minutes = (totalFrames % (fps * 60 * 60)) / (fps * 60);
        return totalFrames;
    }
    //TODO: 要把FramesCode（当前已经渲染的帧数）转化为节拍码
    public static float BpmToTimecode(int bpm, List<int> BpmCode)
    {
        float T = 60 / (float)bpm;//spb
        if (BpmCode[2] == 0)
        {
            return (float)Math.Round(T * BpmCode[0], 2);
        }
        return (float)Math.Round(T * BpmCode[0] + T * BpmCode[1] / BpmCode[2], 2);
    }
    
}