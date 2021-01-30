using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoCellManager : MonoBehaviour
{
    public static StageTwoCellManager instance;
    
    public GameObject LayerOneCell00;
    public GameObject LayerOneCell10;
    public GameObject LayerTwoCell00;
    public GameObject LayerTwoCell10;
    
    private GameObject Character;
    private Transform CharacTransform;
    
    private int xlocation = 0;
    private int ylocation = 0;
    
    //스테이지가 처음 시작할 때는 레이어1이 활성화, 레이어2는 비활성화 되어 있는 상태이다.
    //cellFlag의 인덱스 값이 1이면 레이어 1이 활성화, 2이면 2가 활성화되어있다는 소리이다.
    private int[,] cellFlag = new int[3, 3];
    GameObject[,] layer1 = new GameObject[3,3];
     GameObject[,] layer2 = new GameObject[3,3];
    

    // Start is called before the first frame update
    void Awake()
    {
        FlagInit(); //cellFlag 배열 1로초기화
         Character = GameObject.Find("PlayerCharacter");
         
        if (instance == null) {
            instance = this;
        }

        else {
            Debug.Log("씬에 두 개 이상의 게임매니저가 존재합니다");
            Destroy(gameObject);
        }
    
        // for (int i = 0; i < 3; i++) {
        //     for (int j = 0; i < 3; j++) {

        //     }
        // }
        layer1[0, 0] = LayerOneCell00;
        layer1[1, 0] = LayerOneCell10;
        layer2[0, 0] = LayerTwoCell00;
        layer2[1, 0] = LayerTwoCell10;
    }
    void Update()
    {

    }
    void FlagInit() //cellFlag를 1로 초기화하는 함수.
    {
        for (int i = 0; i < 3; i++) {
            for (int j =0; j < 3; j++) {
                cellFlag[i,j] = 1;
            }
        }
    }

    public void StgTwoCellChange()
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

    public void locationSet()
    {
        CharacTransform = Character.transform;
        Vector3 nowPosition = CharacTransform.position;

        //xlocation의 값을 할당해주는 부분.
        if ((0 <= nowPosition.x) && (nowPosition.x < 10)) {
            xlocation = 0;
        }

        else if ((10 <= nowPosition.x) && (nowPosition.x < 20)) {
            xlocation = 1;
        }

        else if ((20 <= nowPosition.x) && (nowPosition.x < 30)) {
            xlocation = 2;
        }

        //ylocation의 값을 할당해주는 부분.
        if ((nowPosition.y <= 0) && (nowPosition.y >  -10)) { 
            ylocation = 0;
        }
        
        else if ((nowPosition.y <= -10) && (nowPosition.y >  -20)) { 
            ylocation = 1;
        }

        else if ((nowPosition.y <= -20) && (nowPosition.y >  -30)) { 
            ylocation = 2;
        }
    }
}
