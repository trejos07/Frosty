using System;


namespace Frosty.Networking
{
    [Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", x, y, z);
        }
    }
}
