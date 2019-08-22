using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet, shootingPoint;
    [SerializeField] private float sense, forceMagnitude;    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject clonBullet = Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
        Rigidbody clonBody = clonBullet.GetComponent<Rigidbody>();
        clonBody.AddForce(sense * forceMagnitude * transform.right, ForceMode.Impulse);
    }
}
