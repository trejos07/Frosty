using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frosty.Networking;
using Utility;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private float speed = 4;
    [SerializeField] private float rotationSpeed = 60;

    [Header("Object References")]
    [SerializeField] private Transform bulletSpawnpoint;


    [Header("Class References")]
    [SerializeField] private NetworkIdentity networkIdentity;
    [SerializeField] private PlayerController playerController;

    private Vector3 lastRot;
    private Cooldown shootingCooldown ;
    private BulletData bulletData;


    public Vector3 LastRot { get => lastRot;}


    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        shootingCooldown = new Cooldown(1);
        bulletData = new BulletData();
        bulletData.position = new VectorData();
        bulletData.direction = new VectorData();
    }
    void Update()
    {
        if (networkIdentity.IsControlling)
        {
            playerController.ExecuteMove();
            CheckRot();
            CheckShooting();

        }
    }

    private void CheckRot(){

        Vector3 rot = transform.localRotation.eulerAngles + new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") ,0) * rotationSpeed * Time.deltaTime;
        SetRotation(rot);
        lastRot = rot; 
       
    }

    public void SetRotation(Vector3 rot)
    {
        transform.rotation = Quaternion.Euler(rot);
    }

    private void CheckShooting(){

        shootingCooldown.CooldownUpdate();
        if (Input.GetKeyDown(KeyCode.Space)&& !shootingCooldown.OnCooldown) {
            shootingCooldown.StartCooldown();

            bulletData.position = bulletSpawnpoint.position.TwoDecimals();
            bulletData.direction = bulletSpawnpoint.parent.forward.TwoDecimals();

            networkIdentity.Socket.Emit("fireBullet", new JSONObject(JsonUtility.ToJson(bulletData)));
        }
    }
}
