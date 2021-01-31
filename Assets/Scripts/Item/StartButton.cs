using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null) 
                    playerController.hasStart = true;
            }
    }
}
