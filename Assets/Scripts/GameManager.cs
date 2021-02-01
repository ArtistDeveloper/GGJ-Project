using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 스크립트
    public TalkManager talkManager;
    // 대화창
    public GameObject talkPanel;
    public Text UITalkText;
    public GameObject scanObject;       //stage번호 불러올 객체
    public int talkIndex;
    public GameObject ballunAndTail;
    
    // ExitPanel 캔버스 불러오기
    public GameObject creditWindow;
    public GameObject exitPanel;
    public GameObject noEndImage;
    public GameObject soundButton;
    public GameObject playButton;
    public GameObject xButton;



    void Awake()
    {
        ballunAndTail.SetActive(false);
        //if(Application.platform == RuntimePlatform.Android)
        // if(Input.GetKeyDown(KeyCode.Escape)) {    //이거 나중에 X버튼 눌렀을 때로 변경해줘야 함
        //     Time.timeScale = 0f;
        //     //ExitPanel.SetActive(true);
        // }

        if (instance == null) {
            instance = this;
        }

        else {
            Debug.Log("씬에 두 개 이상의 게임매니저가 존재합니다");
            Destroy(gameObject);
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        TalkAction(scanObject);
        ViewSetStart();
        ViewSetSound();
        OnXButton();
    }

    public void TalkAction(GameObject scanObj)
    {
        ballunAndTail.SetActive(true);
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

    //---------- 게임 종류 관련 함수들---------------------
    // X버튼 눌러지면
    public void XButtonClicked()
    {
        Debug.Log("나 눌러졌다~ -x");
        exitPanel.SetActive(true);
    }

    // --------- ExitPane창에 있는 버튼들 눌러지면 -----------
    // Yes버튼 눌러지면
    public void ExitYesButtonClicked()
    {
        Debug.Log("나 눌러졌다~ -ExitYes");
        // 게임 종료
        Application.Quit();
    }
    // No버튼 눌러지면
     public void ExitNoButtonClicked()
    {
        Debug.Log("나 눌러졌다~ -ExitNo");
        noEndImage.SetActive(true);
        
    }
    public void ExitCloseClicked()
    {
        Debug.Log("나 눌러졌다~ -ExitClose");
        exitPanel.SetActive(false);
    }
    public void EndingButtonClicked()
    {
        Debug.Log("나 눌러졌다~ -Ending");
        noEndImage.SetActive(false);
    }
    
    // --------- 아이템 인벤토리 -------------
    public void OnXButton()
    {
        PlayerController player = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();

        if(player.hasEnd == true)
            xButton.SetActive(true);
    }

    public void CreditButtonClicked()
    {
        creditWindow.SetActive(true);
    }
    public void CreditWindowImageClicked()
    {
        creditWindow.SetActive(false);
    }

    public void ViewSetStart()
    {
        PlayerController player = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();

        if(player.hasStart == true)
            playButton.SetActive(true);
        else 
            playButton.SetActive(false);

    }
    public void ViewSetSound()
    {
        PlayerController player = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();

        if(player.hasSound == true)
            soundButton.SetActive(true);
        else 
            soundButton.SetActive(false);

    }
   
}
