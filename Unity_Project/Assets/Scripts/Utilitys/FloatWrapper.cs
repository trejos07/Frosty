using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct FloatWrapper : ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public float value;
    [SerializeField]
    private int _value;

    public FloatWrapper(float value)
    {
        this.value = value;
        _value = (int)(value * 1000);
    }

    public void OnAfterDeserialize()
    {
        value = _value / 1000f;
    }
    public void OnBeforeSerialize()
    {
        _value = (int)(value * 1000);
    }
}