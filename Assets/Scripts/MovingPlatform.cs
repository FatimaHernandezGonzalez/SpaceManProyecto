using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Sobreescribir el metodo 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
