using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;          //적 이동 속도
    public string direction = "left";   //적 이동 방향
    public float range = 0.0f;          //적 이동 범위
    Vector3 defPos;                     //적 시작 위치
    Vector3 enemyScale;                 //적 크기

    void Start()
    {
        //방향 초기화
        SetDirection(direction);
        //시작위치 초기화
        defPos = transform.position;
    }

    void Update()
    {
        // 예를 들어 범위가 8이면 시작위치에서 왼쪽으로 4만큼, 오른쪽으로 4만큼 이동할 수 있음
        // 즉 위치가 range/2를 넘어가면 범위 끝에 도달했으므로 반대방향으로 전환시킴
        if (range > 0.0f)
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

    //------------------- 적 속도 갱신 ------------------------------
    void FixedUpdate()
    {
        //속도 갱신
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        if (direction == "left")
        {
            rbody.linearVelocity = new Vector2(speed,rbody.linearVelocity.y);
        }
        else
        {
            rbody.linearVelocity = new Vector2(-speed, rbody.linearVelocity.y);
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
        enemyScale = transform.localScale;

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
