using System;
using UnityEngine;

namespace Frosty.Networking
{
    [Serializable]
    public class Position : ISerializationCallbackReceiver
    {
        [NonSerialized] public float x;
        [NonSerialized] public float y;
        [NonSerialized] public float z;


        [SerializeField]public string _x;
        [SerializeField] public string _y;
        [SerializeField] public string _z;

        public void OnAfterDeserialize()
        {
            x = float.Parse(_x);
            y = float.Parse(_y);
            z = float.Parse(_z);
        }

        public void OnBeforeSerialize()
        {
            _x = x.ToString();
            _y = y.ToString();
            _z = z.ToString();
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", x, y, z);
        }
    }
}
