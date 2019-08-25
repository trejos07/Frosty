using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frosty.Scriptables { 
    [CreateAssetMenu(fileName ="Server Objects",menuName ="Scriptable Objects/ServerObjects",order =3)]
    public class ServerObjects : ScriptableObject
    {
        [SerializeField]List<ServerObjectData> objects;

        public List<ServerObjectData> Objects { get => objects;private set => objects = value; }

        public ServerObjectData GetObjectByName(string name){
            return Objects.SingleOrDefault(x=> x.Name == name);
        }

    }   

    [Serializable]
    public class ServerObjectData
    {
        public string Name = "New Object";
        public GameObject Prefab;
    } 
}
