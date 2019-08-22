using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour, IController
{
    public float MoveHorizontal() //Applies the interface command to move with PC inputs
    {
        return Input.GetAxis("Horizontal");
    }
}
