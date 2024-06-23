namespace DremuGodot.Script.UniLib;

public interface RecyclableObject {

    /// <summary>
    /// 在物体从对象池中取出时会自动调用，用于初始化物体。
    /// </summary>
    void OnInitialize();

    /// <summary>
    /// 在物体放回对象池时会自动调用。
    /// </summary>
    void OnRecycle();
}

public class ObjectPool
{
    
}