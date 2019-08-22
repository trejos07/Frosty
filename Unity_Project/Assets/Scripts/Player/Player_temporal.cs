using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_temporal : MonoBehaviour, IHurtable
{
    [SerializeField] private float life;
    [SerializeField] Slider lifeBar;
    // Start is called before the first frame update
    void Start()
    {
        lifeBar.maxValue = life;
        lifeBar.value = life;
    }
    public void LoseHealth(float _damage)
    {
        life -= _damage;
        lifeBar.value = life;
        print(life);
        if(life <= 0)
        {
            print("Dead");
        }
    }

    
}
