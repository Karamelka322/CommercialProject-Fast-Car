using System;

namespace CodeBase.Logic.Player
{
    public interface IAffectPlayerDefeat
    {
        event Action OnDefeat;
    }
}