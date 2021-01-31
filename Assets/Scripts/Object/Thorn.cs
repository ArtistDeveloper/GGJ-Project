using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) 
            {
                playerController.Die();
                GameObject player = other.gameObject;
                //Destroy를 통해 여러개의 플레이어 오브젝트가 생기는 것을 방지.
                // Destroy(player); 
            }
        }
    }
}
