using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 이동")]
    public float speed = 3.0f;      //이동 속도
    public float jump = 9.0f;       //점프 강도

    [Header("플레이어 상호작용")]
    public LayerMask groundLayer;               //착지 가능 레이어
    //플레이어의 상태 추가
    public static string gameState = "playing"; //게임 중 상태
    public int score = 0;                       //획득 점수

    bool goJump = false;            //플레이어가 점프 중인지
    bool onGround = false;          //플레이어가 지면에 있는지
    Rigidbody2D rbody;
    float axisH = 0.0f;             //입력
    Animator animator;              //애니메이터 추가


    //----------------- 초기화 ---------------------
    void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameState = "playing";
    }

    //---------------- 플레이어 이동 --------------------
    void Update()
    {
        //플레이어가 오버, 클리어 등 움직일 수 없는 상태이면 Update 종료
        if (gameState != "playing")
            return;

        axisH = Input.GetAxisRaw("Horizontal"); //수평 입력 확인

        if (axisH > 0.0f)
        {
            Debug.Log("오른쪽 이동 눌림");
            transform.localScale = new Vector3(0.8f, 0.8f, 0);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("왼쪽 이동 눌림");
            transform.localScale = new Vector3(-0.8f, 0.8f, 0);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //Animator 값 전달
        animator.SetBool("isMove", axisH != 0);
        animator.SetBool("isJump", !onGround);
    }

    //--------------------- 플레이어 접촉 상호작용 (지면) ---------------------
    private void FixedUpdate()
    {
        //플레이어가 오버, 클리어 등 움직일 수 없는 상태이면 Update 종료
        if (gameState != "playing")
            return;

        //착지 판정
        //플레이어와 지면이 접촉하는지 LineCast로 확인
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //플레이어가 지면 위에 있거나 움직이고 있을 때
        if (onGround || axisH != 0)
        {
            //속도 갱신
            rbody.linearVelocity = new Vector2(speed * axisH, rbody.linearVelocity.y);
        }
        //지면 위에서 점프키가 눌렸을 때
        if(onGround && goJump)
        {
            Debug.Log("점프 중");
            Vector2 jumpPw = new Vector2(0, jump);       //점프 벡터 생성
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); //순간 힘으로 jmpPw만큼 추가 이동
            goJump = false;
        }
    }

    //------------------- 플레이어 접촉 상호작용 (클리어, 데드, 점수) -----------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 골에 닿았을 때
        if (collision.gameObject.tag == "Goal")
            Goal();

        //플레이어가 적 오브젝트나 떨어졌을 때
        else if (collision.gameObject.tag == "Dead")
            GameOver();

        //플레이어가 점수 아이템에 닿았을 때 점수 획득
        else if (collision.gameObject.tag == "ScoreItem")
        {
            //ItemScore에서 점수 오브젝트 가져오고 플레이어와 닿으면 삭제
            ItemData itemScore = collision.gameObject.GetComponent<ItemData>();
            score = itemScore.value;
            Destroy(collision.gameObject);
        }
    }

    //------------------- 점프 켜기 --------------------
    public void Jump()
    {
        goJump = true;
        Debug.Log("점프 키 눌림");
    }

    public void Goal()
    {
        animator.SetBool("isGoal", true);
        gameState = "gameclear";
        GameStop();
        Debug.Log("플레이어 상태 : gameclear");
    }

    public void GameOver()
    {
        animator.SetBool("isOver", true);
        gameState = "gameover";
        GameStop();
        Debug.Log("플레이어 상태 : gameover");

        //게임 오버 연출
        GetComponent<CapsuleCollider2D>().enabled = false;      //플레이어 충돌 비활성
        rbody.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //플레이어 튀어오르는 연출     
    }

    private void GameStop()
    {
        //플레이어의 속도를 0으로 하기
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.linearVelocity = new Vector2(0,0);
    }
}
