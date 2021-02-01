using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                Debug.Log("나 플레이어에 닿았어! - 스킨");
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null){ 
                    Debug.Log("나 진짜진짜 플레이어에 닿았어! - 스킨");
                    playerController.GetBody();
                    // animator.SetBool("hasBody", true);
                    // animator.SetBool("hasFeet", false);
                    // Debug.Log("hasBody야");
                    // Debug.Log(animator.GetBool("hasBody"));
                    // Debug.Log("hasFeet야");
                    // Debug.Log(animator.GetBool("hasFeet"));
                }
                Destroy(gameObject);
            }
    }
}
