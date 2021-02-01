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

            //other를 통해 player를 들고 옴. 문 직전의 객체면 아이템을 다 먹은 상태일 것이니
            // 이 상태를 가지고 있는 player를 저장하여 .Respawn함수에 사용할 것.
            PlayerController player = other.GetComponent<PlayerController>();

            //스테이지 전체 개수보다 작으면 LoadScene수행
            if (sceneNumber+1 < 6) {
                SceneManager.LoadScene(sceneNumber+1);
                // SceneManager.LoadScene("Stage2");
            }
        }
    }
}
