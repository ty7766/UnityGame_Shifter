using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("적 속성")]
    public float speed = 2.0f;          //적 이동 속도
    public string direction = "left";   //적 이동 방향
    public float range = 0.0f;          //적 이동 범위
    public bool isStopped = false;      //적의 이동 유무
    Vector3 defPos;                     //적 시작 위치

    [Header("연결 스크립트")]
    public ShiftController shiftController;     //배경 색상에 따른 적 제어
        
    void Start()
    {
        //방향 초기화
        SetDirection(direction);
        //시작위치 초기화
        defPos = transform.position;
    }

    void Update()
    {
        //배경이 노을일 때 적 오브젝트 정지
        if (shiftController != null)
        {
            string back = shiftController.GetCurrentBackName();
            isStopped = (back == "back_sunset");
        }

        //배경이 노을일 때 태그를 Ground로 바꾸어 플레이어가 밟아도 Dead 적용이 안되게 구현
        if (isStopped && tag != "Ground")
            tag = "Ground";
        else if (!isStopped && tag != "Dead")
            tag = "Dead";

        //배경이 노을일 때 애니메이션 정지
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("IsMoving", !isStopped);


        // 예를 들어 범위가 8이면 시작위치에서 왼쪽으로 4만큼, 오른쪽으로 4만큼 이동할 수 있음
        // 즉 위치가 range/2를 넘어가면 범위 끝에 도달했으므로 반대방향으로 전환시킴
        if (!isStopped && range > 0.0f)
        {
            if (transform.position.x < defPos.x - (range/2))
            {
                direction = "right";
                SetDirection(direction);
            }
            if (transform.position.x > defPos.x + (range/2))
            {
                direction="left";
                SetDirection(direction);
            }
        }
    }

    //------------------- 적 속도 갱신 -----------------------
    void FixedUpdate()
    {

        //속도 갱신
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        //배경이 sunset일때 적 즉시 멈추기
        if (isStopped)
        {
            rbody.linearVelocityX = 0;
            return;
        }

        if (direction == "left")
        {
            rbody.linearVelocityX = -speed;
        }
        else
        {
            rbody.linearVelocityX = speed;
        }
    }

    //----------------------- 지형 - 적 충돌 알고리즘 ------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적이 지형과 충돌하면 충돌 반대 방향으로 방향 전환
        if (direction == "left")
        {
            direction = "right";
            SetDirection(direction);
        }
        else
        {
            direction = "left";
            SetDirection(direction);
        }
    }

    //-------------- 적 방향 전환 --------------------
    void SetDirection(string direction)
    {
        Vector3 enemyScale = transform.localScale;

        if (direction == "left")
        {
            enemyScale.x = Mathf.Abs(enemyScale.x);
        }
        else
        {
            enemyScale.x = -Mathf.Abs(enemyScale.x);
        }

        transform.localScale = enemyScale;
    }
}
