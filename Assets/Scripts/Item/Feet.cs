using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null) 
                   playerController.GetFeet();
            }
    }
}
