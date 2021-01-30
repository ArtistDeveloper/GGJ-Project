using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    
    // 스크립트
    public TalkManager talkManager;
    // 대화창
    public GameObject talkPanel;
    public Text UITalkText;
    public GameObject scanObject;       //stage번호 불러올 객체
    public int talkIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       TalkAction(scanObject);
    }
    public void TalkAction(GameObject scanObj)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id);
        if(Input.GetKeyDown(KeyCode.Space)==true){
            Debug.Log("스페이스 키 누름");
            talkPanel.SetActive(false);
        }
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null)
        {
            talkIndex = 0;
            return;
        }
       
        UITalkText.text = talkData;

        // 다음 대사 들고오기 위해
        //talkIndex++;
    }
}
