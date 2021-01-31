using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null) 
                    playerController.hasTile = true;
                    //hastile이 true가 됐다. 즉, 타일아이템을 먹은 것이니 retiling메소드를 호출한다.
                    CallRetilingMethod();
            }
    }

    void CallRetilingMethod()
    {
        GameObject.Find("CellManager").GetComponent<StageFiveCellManager>().Retiling();        
    }
}
