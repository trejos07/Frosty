using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Atributes;

namespace Frosty.Networking
{
    public class NetworkIdentity : MonoBehaviour
    {
        [Header("Helpful Values")]
        [SerializeField][GreyOut] private string id;  //SerializeField para exponer el campo en el editor GreyOut para evitar edicion - Es una propiedad Custom
        [SerializeField][GreyOut]private bool isControlling; 

        private SocketIOComponent socket;

        public string Id { get => id;}
        public bool IsControlling { get => isControlling;}
        public SocketIOComponent Socket { get => socket;}

        public void Awake ()
        {
            isControlling = false;
        }


        public void SetControllerID(string ID)
        {
            id = ID;
            isControlling = NetworkClient.ClientID == ID ? true : false; //
        }
        public void SetSocketReference(SocketIOComponent Socket)
        {
            socket = Socket;
        }
    }
}
