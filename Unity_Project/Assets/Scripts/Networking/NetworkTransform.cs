using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Atributes;


namespace Frosty.Networking
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class NetworkTransform : MonoBehaviour
    {

        [SerializeField] [GreyOut] private Vector3 oldPosition;

        private NetworkIdentity networkIdentity;
        private PlayerData player;

        private float stillCounter = 0;

        void Start()
        {
            networkIdentity = GetComponent<NetworkIdentity>();
            oldPosition = transform.position;
            player = new PlayerData();
            player.position = new VectorData();

            player.position.x = 0;
            player.position.y = 0;

            if(!networkIdentity.IsControlling){
                enabled = false;
            }
        }

        void Update()
        {
            if(networkIdentity.IsControlling)
            {
                if(oldPosition != transform.position){

                    oldPosition = transform.position;
                    stillCounter = 0;
                    SendData();

                }
                else {                //regula la tasa de actualizacion si no me estoy moviendo 
                    stillCounter += Time.deltaTime;

                    if (stillCounter >= 1) {

                        stillCounter = 0;
                        SendData();
                    }
                }
            }
        }

        private void SendData()
        {
            //Update player information
            player.position = transform.position.TwoDecimals();

            string json = JsonUtility.ToJson(player);
            networkIdentity.Socket.Emit("updatePosition", new JSONObject(json));
        }
    }
}
