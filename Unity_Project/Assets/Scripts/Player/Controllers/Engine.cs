using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(Rigidbody2D))]
public class Engine : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 4;
    [SerializeField] private float horizontalDisplacementBoundary = 6;

    public void Move(float horizontalInput)
    {
        transform.position += new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -horizontalDisplacementBoundary, horizontalDisplacementBoundary), transform.position.y,
            transform.position.z);
    }
}