using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFiveCellManager : CellManager
{
    public static StageFiveCellManager instance;
    public GameObject RgbLayerOneCell00;
    public GameObject RgbLayerOneCell10;
    public GameObject RgbLayerOneCell20;
    public GameObject RgbLayerTwoCell00;
    public GameObject RgbLayerTwoCell10;
    public GameObject RgbLayerTwoCell20;

    public GameObject TileLayerOneCell00;
    public GameObject TileLayerOneCell10;
    public GameObject TileLayerOneCell20;
    public GameObject TileLayerTwoCell00;
    public GameObject TileLayerTwoCell10;
    public GameObject TileLayerTwoCell20;
    

    // 여기서만 있는 변수
    public GameObject[,] TileLayer1 = new GameObject[3,3];
    public GameObject[,] TileLayer2 = new GameObject[3,3];

    public GameObject RGB_Cells;
    public GameObject BrownPurple_Cells;
    

     void Awake()
    {
        //스테이지2의 리스폰 포지션 지정.
        respawnPosition = new Vector2(1.5f,-2.1f);

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

        //RGB 레이어1
        layer1[0,0] = RgbLayerOneCell00;
        layer1[1,0] = RgbLayerOneCell10;
        layer1[2,0] = RgbLayerOneCell20;

        //RGB 레이어2
        layer2[0, 0] = RgbLayerTwoCell00;
        layer2[1, 0] = RgbLayerTwoCell10;
        layer2[1, 0] = RgbLayerTwoCell20;

        //타일 레이어1
        TileLayer1[0, 0] = TileLayerOneCell00;
        TileLayer1[1, 0] = TileLayerOneCell10;
        TileLayer1[2, 0] = TileLayerOneCell20;

        //타일 레이어2
        TileLayer2[0, 0] = TileLayerTwoCell00;
        TileLayer2[1, 0] = TileLayerTwoCell10;
        TileLayer2[2, 0] = TileLayerTwoCell20;
    }

    public override void CellChange()
    {
        bool hasTile = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>().hasTile;
        Debug.Log(hasTile);
        locationSet(); //x와 y의 location값을 할당시킨다.

        if (hasTile == false) {
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

        else if (hasTile == true) {
                if (cellFlag[xlocation, ylocation] == 1) {
                TileLayer1[xlocation, ylocation].SetActive(false); //레이어1의 해당 위치 셀을 비활성화
                TileLayer2[xlocation, ylocation].SetActive(true); //레이어2의 해당 위치 셀을 활성화
                cellFlag[xlocation, ylocation] = 2;  //
            }

            // 레이어2의 해당되는 셀을 비활성화로 바꾸고, 레이어1의 해당되는 셀을 활성화시켜준다.
            else if (cellFlag[xlocation, ylocation] == 2) {
                TileLayer2[xlocation, ylocation].SetActive(false); //레이어1의 해당 위치 셀을 비활성화
                TileLayer1[xlocation, ylocation].SetActive(true); //레이어2의 해당 위치 셀을 활성화
                cellFlag[xlocation, ylocation] = 1;  //
            }
        }
    }

    public void Retiling()
    {
        //Retiling메소드가 호출될 때는 hastile이 true일때다. 즉, tile아이템을 먹은 것이므로 기존에 RGB셀은 아예 false시켜버린다.
        RGB_Cells.SetActive(false);
        BrownPurple_Cells.SetActive(true);

        // CellFlag에 저장된 내용대로 필요한 TileLayer의 인덱스들을 활성화시킨다.
        // for (int i = 0; i < 3; i++) {
        //     for (int j =0; j < 3; j++) {
        //         // if cellFlag[i,j];
        //         if (cellFlag[i,j] == 1) {
        //             TileLayer1[i, j].SetActive(true);
        //         }

        //         else if (cellFlag[i,j] == 2) {
        //             TileLayer2[i, j].SetActive(true);
        //         }
        //     }
        // }
    }

}
