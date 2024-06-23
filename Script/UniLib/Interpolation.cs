using System;
using GdUnit4;

namespace DremuGodot.Script.UniLib;

public class Interpolation
{
    
    /// <summary>
    /// 线性插值函数
    /// </summary>
    /// <param name="k">斜率</param>
    /// <param name="t">插值</param>
    /// <returns>返回的线性插值</returns>
    public static double Liner(double k, double t)
    {
        return Math.Round(k*t,2);
    }
    
    /// <summary>
    /// Sine曲线插值
    /// </summary>
    /// <param name="A">振幅</param>
    /// <param name="omiga">Omiga的值</param>
    /// <param name="t">插值</param>
    /// <returns>返回的Sine插值</returns>
    public static double SineLerp(double A, double omiga, double t)
    {
        return A * Math.Sin(omiga * t);
    }
    
    /// <summary>
    /// 二次函数插值
    /// </summary>
    /// <param name="t">插值</param>
    /// <returns>返回的二次插值</returns>
    public static double QuadraticFunctions(double t)
    {
        return t * t;
    }
}