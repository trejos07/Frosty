using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace Frosty.Networking{
    public class NetworkClient : SocketIOComponent
    {
        public override void Start()
        {
            base.Start();
        }

        public override  void Update()
        {
            base.Update();

        }

        public void SetupEvents(){
            On("Open", (E) => {
                Debug.Log("connection made to the server");
            });
        }

    }
}

