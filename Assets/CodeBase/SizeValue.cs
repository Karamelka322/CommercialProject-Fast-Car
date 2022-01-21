using System;

namespace CodeBase
{
    [Serializable]
    public struct SizeValue
    {
        public int Width;
        public int Height;

        public SizeValue(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}