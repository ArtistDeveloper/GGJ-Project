using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoCellManager :  CellManager
{
    public static StageTwoCellManager instance;
    
    //유니티 인스펙터 창에서 값을 할당해준다.
    public GameObject LayerOneCell00;
    public GameObject LayerOneCell10;
    public GameObject LayerTwoCell00;
    public GameObject LayerTwoCell10;

    // GameObject[,] layer1 = new GameObject[3,3];
    // GameObject[,] layer2 = new GameObject[3,3];
    
    void Awake()
    {
        //스테이지2의 리스폰 포지션 지정.
        respawnPosition = new Vector2(5,-7);

        //플레이어 캐릭터 받아오기.
        Character = GameObject.Find("PlayerCharacter");
        Character.transform.position = respawnPosition;

        //cellFlag 배열 1로초기화
        FlagInit(); 

        if (instance == null) {
            instance = this;
        }

        else {
            Debug.Log("씬에 두 개 이상의 게임매니저가 존재합니다");
            Destroy(gameObject);
        }

        layer1[0, 0] = LayerOneCell00;
        layer1[1, 0] = LayerOneCell10;
        layer2[0, 0] = LayerTwoCell00;
        layer2[1, 0] = LayerTwoCell10;
    }

    public override void CellChange()
    {
        locationSet(); //x와 y의 location값을 할당시킨다.

        // 레이어1의 해당되는 셀을 비활성화로 바꾸고, 레이어2의 해당되는 셀을 활성화시켜준다.
        if (cellFlag[xlocation, ylocation] == 1) {
            layer1[xlocation, ylocation].SetActive(false); //레이어1의 해당 위치 셀을 비활성화
            layer2[xlocation, ylocation].SetActive(true); //레이어2의 해당 위치 셀을 활성화
            cellFlag[xlocation, ylocation] = 2;  //
        }

        // 레이어2의 해당되는 셀을 비활성화로 바꾸고, 레이어1의 해당되는 셀을 활성화시켜준다.
        else if (cellFlag[xlocation, ylocation] == 2) {
            layer2[xlocation, ylocation].SetActive(false); //레이어1의 해당 위치 셀을 비활성화
            layer1[xlocation, ylocation].SetActive(true); //레이어2의 해당 위치 셀을 활성화
            cellFlag[xlocation, ylocation] = 1;  //
        }
    }
}
