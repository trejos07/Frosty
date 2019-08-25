using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Frosty.Networking;
using Frosty.Scriptables;

namespace Frosty.Networking{
    public class NetworkClient : SocketIOComponent
    {
        [Header("Network Cliente")]
        [SerializeField] private Transform networkContiner;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private ServerObjects serverSpawnables;

        public static string ClientID { get; private set; }

        private Dictionary<string, NetworkIdentity> serverObjects; // reprecentacion del mismo dicionario de objetos del servidor 

        public override void Start()
        {
            base.Start();
            initialize();
            SetupEvents();
        }

        public override  void Update()
        {
            base.Update();
        }

        public void initialize(){
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }

        public void SetupEvents(){
            On("open", (E) => {
                Debug.Log("connection made to the server");
            });

            On("register", (E) =>{
                ClientID = E.data["id"].ToString().RemoveQuotes();
                Debug.LogFormat("Our Client's ID ({0})", ClientID); 
            });

            On("spawn", (E) => {
                string id = E.data["id"].ToString().RemoveQuotes();
                GameObject go = Instantiate(playerPrefab, networkContiner);
                go.name = string.Format("Player ({0})",id);
                go.transform.SetParent(networkContiner);
                NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
                ni.SetControllerID(id);
                ni.SetSocketReference(this);
                serverObjects.Add(id, ni);
            });

            On("updatePosition", (E) => {

                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["position"]["x"].str.ParseFloat();
                float y = E.data["position"]["y"].str.ParseFloat();
                float z = E.data["position"]["z"].str.ParseFloat();


                Vector3 pos = new Vector3(x, y, z);
                //Debug.Log(string.Format("player {0} moves to {1}",id,pos));

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = pos;
            });

            On("updateRotation", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["rotation"]["x"].str.ParseFloat();
                float y = E.data["rotation"]["y"].str.ParseFloat();
                float z = E.data["rotation"]["z"].str.ParseFloat();

                Vector3 rot = new Vector3(x, y, z);


                NetworkIdentity ni = serverObjects[id];
                ni.GetComponent<Player>().SetRotation(rot);
            });

            On("serverSpawn", (E) =>{

                string name = E.data["name"].str;
                string id = E.data["id"].ToString().RemoveQuotes();
                

                Debug.LogFormat("Server wants us to Spawn a '{0}'", name);
                if(!serverObjects.ContainsKey(id))
                {
                    ServerObjectData sod = serverSpawnables.GetObjectByName(name);
                    var spawnwedObject = Instantiate(sod.Prefab, networkContiner);

                    float x = E.data["position"]["x"].str.ParseFloat();
                    float y = E.data["position"]["y"].str.ParseFloat();
                    float z = E.data["position"]["z"].str.ParseFloat();

                    spawnwedObject.transform.position = new Vector3(x, y, z);
                    var ni = spawnwedObject.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);

                    if(name == "Bullet"){
                        float dir_x = E.data["direction"]["x"].str.ParseFloat();
                        float dir_y = E.data["direction"]["y"].str.ParseFloat();
                        float dir_z = E.data["direction"]["z"].str.ParseFloat();
                        spawnwedObject.transform.rotation = Quaternion.LookRotation(new Vector3(dir_x, dir_y, dir_z),Vector3.up);
                    }

                    serverObjects.Add(id, ni);
                }

            });

            On("serverUnspawn", (E) => {
                string id = E.data["id"].ToString().RemoveQuotes();
                NetworkIdentity ni = serverObjects[id];
                serverObjects.Remove(id);
                DestroyImmediate(ni.gameObject);

            });


            On("disconnected", (E) => {
                string id = E.data["id"].ToString().RemoveQuotes();

                GameObject go = serverObjects[id].gameObject;
                Destroy(go);
                serverObjects.Remove(id);

            });

            

        }

    }
}


[Serializable]
public class BulletData
{
    public string id;
    public VectorData position; 
    public VectorData direction; 

}
