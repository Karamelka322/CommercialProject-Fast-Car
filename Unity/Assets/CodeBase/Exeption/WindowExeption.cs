using System;

namespace CodeBase.Services.Window
{
    public class WindowExeption : Exception
    {
        public WindowExeption(string message) : base(message) { }
    }
}