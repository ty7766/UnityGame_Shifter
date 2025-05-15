using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //입력
    Animator animator;      //애니메이터 추가

    public float speed = 3.0f;      //이동 속도
    public float jump = 9.0f;       //점프 강도
    public LayerMask groundLayer;   //착지 가능 레이어
    
    bool goJump = false;            //플레이어가 점프 중인지
    bool onGround = false;          //플레이어가 지면에 있는지

    //----------------- 초기화 ---------------------
    void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
    private void FixedUpdate()
    {
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

    //------------------- 플레이어 접촉 상호작용 (클리어, 데드)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
            Goal();
        else if (collision.gameObject.tag == "Dead")
            GameOver();
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
    }

    public void GameOver()
    {
        animator.SetBool("isOver", true);
    }
}
