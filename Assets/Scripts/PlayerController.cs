using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float maxSpeed;
    //public GameManager manager; // TalkManager 관련
    //public GameObject scanObject;       // TalkManager에서 쓸 Object 불러오기용
    private Transform playerPosition;
    private Rigidbody2D rigid;
    public float movePower = 3f;
    public SpriteRenderer rend;
    public float jumpPower = 7f;
    private int jumpCount = 0;
    Animator animator;
    // 아이템 상태 저장 변수
    public bool hasTile = false;
    public bool hasSound = false;
    public bool hasEnd = false;
    public bool hasStart = false;
    // 소리 추가
    public AudioClip audioJump;     //EtcSound
    public AudioClip audioWalking;   
    public AudioClip audioSpeaking;
    public AudioClip audioDie;
    public AudioClip audioCellConversion;
    public AudioClip audioNextStage;
    public AudioClip audioPressNoEndButton;
    public AudioClip audioButtonClick;
    public AudioClip audioBackground;
    AudioSource audioSource;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }

        else {
            Debug.Log("씬에 두 개 이상의 게임매니저가 존재합니다");
            Destroy(gameObject);
        }

        rigid = GetComponent<Rigidbody2D>();
        playerPosition = GetComponent<Transform>();
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        // PlaySound("Background");
        // audioSource.Play();
        
    }

    void Start()
    {
        //플레이어 오브젝트가 파괴되지 않도록 함.
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Jump
        // if((Input.GetKeyDown(KeyCode.DownArrow)) {
        //     if (hasFeet) {
        //         anim.SetBool("isJumping", true);
        //         //isJumping = true;
        //     } 
        //     else {
        //         Debug.Log("발이 없어서 뛸 수 없습니다.ㅠㅠ");
        //     }
        // }
        if (jumpCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (animator.GetBool("hasFeet")) {
                //     anim.SetBool("isJumping", true);
                //     isJumping = true;
                // } 
                    jumpCount++;
                    animator.SetBool("isJumping", true);
                }
                else {
                    Debug.Log("발이 없어서 뛸 수 없습니다.ㅠㅠ");
                }
                   
           }       
        }
    }
    void FixedUpdate()
    {
        // 말하기함수
       // manager.TalkAction();
        PlayerMove();
        PlayerJump();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die(); //restart
        }
        
        //셀 전환
        //stage1, 3, 4, 5에도 이것처럼 일일히 if를 사용해서 호출하기.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(playerPosition.position.x); //플레이어 포지션 디버깅용
            Debug.Log(playerPosition.position.y);

            if (SceneManager.GetActiveScene().name == "Stage2") {                
                GameObject.Find("CellManager").GetComponent<StageTwoCellManager>().CellChange();
            }

            else if (SceneManager.GetActiveScene().name == "Stage3") {
                GameObject.Find("CellManager").GetComponent<StageThreeCellManager>().CellChange();
            }

            else if (SceneManager.GetActiveScene().name == "Stage4") {
                GameObject.Find("CellManager").GetComponent<StageFourCellManager>().CellChange();
            }

            else if (SceneManager.GetActiveScene().name == "Stage5") {
                GameObject.Find("CellManager").GetComponent<StageFiveCellManager>().CellChange();
            }
        }

        // Landing Platform 바닥 착지
        if(rigid.velocity.y < 0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));   //빔쏘기
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));    //   레이저가 바닥에 맞았으면
            
            if(rayHit.collider != null){    // 무언가에 맞았다!!
                if(rayHit.distance < 0.5f)  // 바닥과의 거리
                    animator.SetBool("isJumping", false);
                    jumpCount = 0;
            }  
        }
        
    }

    void PlayerMove() 
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            rend.flipX = true; // flip X를 true로 변경해 좌우반전   
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            rend.flipX = false; // flipX 값을 원위치
        }
        if (hasSound == true)
            // PlaySound("Die");
            // audioSource.Play();
            Debug.Log("나지금 걷는 소리 재생중인데 안들리니?");
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }

    void PlayerJump()
    {
        if( !animator.GetBool("isJumping"))
            return;
        // 점프 직전에 속도를 순간적으로 제로(0,0)로 변경
        rigid.velocity = Vector2.zero;	

        // 리지드바디에 위쪽으로 힘 주기
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        // 오디오 소스 재생
        if (hasSound == true)
            // PlaySound("Background");
            // audioSource.Play();
            Debug.Log("나도 점프 소리 재생중인데 안들리니?");
        animator.SetBool("isJumping", false);
    }

    public void Die()   
    {
        // gameobject는 자기자신을 가리킴. == PlayerController니까 playerCharacter를 가리킴.
        // !! 근데 stage1에서는 Restart를 할 때 DontDestroyOnLoad때문에 Restart를 하면 
        // !!  플레이어가 파괴되지 않고 계속 늘어나는 현상이 있음. 근데 다음 씬으로 이동하면 DontDestroy가 제대로 작동하지 않는 것 같음.
        // Destroy(gameObject);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }
    
    //사운드 재생
    void PlaySound(string soundCheck)
    {
        switch(soundCheck){
        case "Jump": audioSource.clip = audioJump; break;
        case "Walk": audioSource.clip = audioWalking; break;
        case "Speaking": audioSource.clip = audioSpeaking; break;
        case "Die": audioSource.clip = audioDie; break;
        case "CellConversion": audioSource.clip = audioCellConversion; break;
        case "NextStage": audioSource.clip = audioNextStage; break;
        case "PressNoEndButton": audioSource.clip = audioPressNoEndButton; break;
        case "ButtonClick": audioSource.clip = audioButtonClick; break;
        case "Background": audioSource.clip = audioBackground; break;
        }
    }

// ------- 아이템 획득!!! ----------
    public void GetBody()
    {
        animator.SetBool("hasBody", true);
        animator.SetBool("hasFeet", false);
        // Debug.Log(animator.GetBool("hasBody"));
        // Debug.Log(animator.GetBool("hasFeet"));
    }
    public void GetFeet()
    {
        animator.SetBool("hasBody", true);
        animator.SetBool("hasFeet", true);
    }
    public void GetSound()
    {
        hasSound = true;
    }
    public void GetStart(){
        hasStart = true;
    }
    public void GetEnd(){
        hasEnd = true;
    }
}

