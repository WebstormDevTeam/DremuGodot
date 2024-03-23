using System.Collections.Generic;

namespace DremuGodot.Script.GamePlayer;

public interface INote
{
    void Create(int number,List<int> BeatCode);
    void JudgeAndDel(double TouchTime);
}