using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int xlocation = 0;
    public int ylocation = 0;
    
    //스테이지가 처음 시작할 때는 레이어1이 활성화, 레이어2는 비활성화 되어 있는 상태이다.
    //cellFlag의 인덱스 값이 1이면 레이어 1이 활성화, 2이면 2가 활성화되어있다는 소리이다.
    public int[,] cellFlag = new int[3, 3];

    public GameObject Character;
    public Transform CharacTransform;
    public Vector2 respawnPosition;

    public GameObject[,] layer1 = new GameObject[3,3];
    public GameObject[,] layer2 = new GameObject[3,3];

    public abstract void CellChange();
    public void FlagInit() //cellFlag를 1로 초기화하는 함수.
    {
        for (int i = 0; i < 3; i++) {
            for (int j =0; j < 3; j++) {
                cellFlag[i,j] = 1;
            }
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
