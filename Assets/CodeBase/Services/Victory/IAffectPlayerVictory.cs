using System;

namespace CodeBase.Services.Victory
{
    internal interface IAffectPlayerVictory
    {
        event Action OnVictory;
    }
}