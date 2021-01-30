using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    //public GameManager manager; // TalkManager 관련
    //public GameObject scanObject;       // TalkManager에서 쓸 Object 불러오기용
    private Transform playerPosition;
    private Rigidbody2D rigid;
    public float movePower = 1f;
    public SpriteRenderer rend;
    public float jumpPower = 128f;
    bool isJumping = false;
    bool hasBody = true;
    bool hasFeet = true;
    bool hasTile = false;
    bool hasBackground = false;
    bool hasSound = false;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerPosition = GetComponent<Transform>();
        //플레이어 오브젝트가 파괴되지 않도록 함.
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Jump
        if(Input.GetAxisRaw("Vertical")>0){
            if(hasFeet)
                isJumping = true;
            else
                Debug.Log("발이 없어서 뛸 수 없습니다.ㅠㅠ");
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
                GameObject.Find("CellManager").GetComponent<StageTwoCellManager>().StgTwoCellChange();
            }
        }

        // Landing Platform
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));   //빔쏘기
        
        RaycastHit2D rayHit = Physis2D.Raycast(rigid.position, Vector3.down,1);
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
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }

    void PlayerJump()
    {
        if( !isJumping)
            return;
        // 점프 직전에 속도를 순간적으로 제로(0,0)로 변경
        rigid.velocity = Vector2.zero;	

        // 리지드바디에 위쪽으로 힘 주기
        //Vector2 jumpVelocity = new Vector2(0, jumpPower);
      //  rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
    
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        // 책에서의 jumpPower는 700f

        // 오디오 소스 재생
       // if ( hasSound)
         //   PlayerAudio.Play();
        isJumping = false;    
    }

    public void Die()
    {
        //.SetActice를 false시키면 No Camera Rendering이 화면에 뜨는 문제가 생기기 때문에 LoadScene만 다시해주면 됨.
        // gameObject.SetActive(false); 
        // gameobject는 자기자신을 가리킴.
        //씬 처음부터 다시시작
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }
}
