using System;
using UnityEngine;

namespace Frosty.Networking
{
    [Serializable]
    public class VectorData : ISerializationCallbackReceiver
    {
        [NonSerialized] public float x;
        [NonSerialized] public float y;
        [NonSerialized] public float z;


        [SerializeField]public string _x;
        [SerializeField] public string _y;
        [SerializeField] public string _z;

        public VectorData(float x=0, float y=0, float z=0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator VectorData(Vector3 v) => new VectorData(v.x, v.y, v.z);

        public void OnAfterDeserialize()
        {
            x = float.Parse(_x);
            y = float.Parse(_y);
            z = float.Parse(_z);
        }
        public void OnBeforeSerialize()
        {
            _x = x.ToString().Replace(",",".");
            _y = y.ToString().Replace(",", ".");
            _z = z.ToString().Replace(",", ".");
        }
        public override string ToString()
        {
            return string.Format("({0},{1},{2})", x, y, z);
        }

    }
}
