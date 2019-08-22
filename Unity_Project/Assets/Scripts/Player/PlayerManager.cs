using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frosty.Networking;


[RequireComponent(typeof(PlayerController))]
public class PlayerManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private float speed = 4;

    [Header("Class References")]
    [SerializeField] private NetworkIdentity networkIdentity;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (networkIdentity.IsControlling)
        {
            playerController.ExecuteMove();
        }
    }
}
