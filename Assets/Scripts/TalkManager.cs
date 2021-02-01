using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;     // 대화 데이터에 대한 사전

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();     // 데이터 생성
    }

    void GenerateData()
    {   
        // id: stage번호
        // 대화 생성
        // id = 0: stage1
        talkData.Add(1, new string[] { "Where is the door?" });
        talkData.Add(2, new string[] { "Where is the dummy map?" });
        talkData.Add(3, new string[] { "Where is my torso?" });
        talkData.Add(4, new string[] { "Where are my feet?" });
        talkData.Add(5, new string[] { "Where are the background tiles and X?" });
        // talkData.Add(7, new string[] { "Where is the sound?" });
        // talkData.Add(11, new string[] { "Where is the X?" });
        

    }
    // 대사 가져오기
    public string GetTalk(int id, int talkIndex)    // Object의 id, string배열(대사)의 index
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
