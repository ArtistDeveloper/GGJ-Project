using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageThreeCellManager : CellManager
{
    public static StageThreeCellManager instance;

    public GameObject LayerOneCell00;
    public GameObject LayerOneCell01;
    public GameObject LayerOneCell02;
    public GameObject LayerOneCell12;
    public GameObject LayerTwoCell00;
    public GameObject LayerTwoCell01;
    public GameObject LayerTwoCell02;
    public GameObject LayerTwoCell12;

    void Awake()
    {
        //스테이지2의 리스폰 포지션 지정.
        respawnPosition = new Vector2(7,-2);

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

        //레이어1
        layer1[0, 0] = LayerOneCell00;
        layer1[0, 1] = LayerOneCell01;
        layer1[0, 2] = LayerOneCell02;
        layer1[1, 2] = LayerOneCell12;

        //레이어2
        layer2[0, 0] = LayerTwoCell00;
        layer2[0, 1] = LayerTwoCell01;
        layer2[0, 2] = LayerTwoCell02;
        layer2[1, 2] = LayerTwoCell12;
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
