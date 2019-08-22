using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;

    public void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.GetComponent<IHurtable>() != null)
        {
            _collision.gameObject.GetComponent<IHurtable>().LoseHealth(damage);
            Destroy(gameObject);
        }
    }    
    
}
