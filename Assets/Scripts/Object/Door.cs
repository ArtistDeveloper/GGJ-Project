using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//씬 이름 배열에 넣고 거기로 이동시키도록 하기.
public class Door : MonoBehaviour
{
    private int sceneNumber;

    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true))
        {
            Debug.Log("Door Enter");
            Debug.Log(sceneNumber);

            //스테이지 전체 개수보다 작으면 LoadScene수행
            if (sceneNumber+1 < 5) {
                SceneManager.LoadScene(sceneNumber+1);
            }
        }
    }
}
