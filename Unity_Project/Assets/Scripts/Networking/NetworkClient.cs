using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace Frosty.Networking{
    public class NetworkClient : SocketIOComponent
    {
        [Header("Network Cliente")]
        [SerializeField] private Transform networkContiner;
        [SerializeField] private GameObject playerPrefab;

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

                string id = E.data["id"].ToString().RemoveQuotes(); ;
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x,y,z);
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

