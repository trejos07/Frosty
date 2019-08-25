using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Atributes;

namespace Frosty.Networking {
    public class NetworkRotation : MonoBehaviour
    {
        [Header("Reference Values")]
        [SerializeField] [GreyOut] private Vector3 oldRotation;

        [Header("Class References")]
        [SerializeField] private Player playerManager;

        private NetworkIdentity identity;
        private VectorData playerRot;
        private float stillCounter = 0;


        void Start()
        {
            identity = GetComponent<NetworkIdentity>();
            playerManager = GetComponent<Player>();

            playerRot = new VectorData();
            playerRot.x = 0;
            playerRot.z = 0;
            playerRot.x = 0;

            enabled = identity.IsControlling;

        }

        void Update()
        {
            if(identity.IsControlling)
            {
                if(oldRotation != transform.eulerAngles)
                {
                    oldRotation = transform.eulerAngles;
                    stillCounter = 0;
                    SendData();
                } else {

                    stillCounter += Time.deltaTime;
                    if(stillCounter>=1)
                    {
                        stillCounter = 0;
                        SendData();
                    }
                }

            }
        }

        private void SendData(){

            playerRot.x = transform.localEulerAngles.x.TwoDecimals(); 
            playerRot.y = transform.localEulerAngles.y.TwoDecimals(); 
            playerRot.z = transform.localEulerAngles.z.TwoDecimals();
            string json = JsonUtility.ToJson(playerRot);
            //Debug.Log(json);
            identity.Socket.Emit("updateRotation", new JSONObject(json));
        }

    }
}
