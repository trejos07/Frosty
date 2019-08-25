using System;


namespace Frosty.Networking
{
    [Serializable]
    public class PlayerData
    {
        public string id;
        public VectorData position;
        public VectorData rotation;
    }
}
