using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("블록 속성")]
    public float speed = 2.0f;          // 블록 이동 속도
    public float range = 4.0f;          // 블록 이동 범위
    public string direction = "right";  // 이동 방향
    public bool isStopped = false;      // 블록 이동 유무
    private Vector3 defPos;             // 시작 위치
    private GameObject playerOnTop;     // 올라온 플레이어 참조

    [Header("연결 스크립트")]
    public ShiftController shiftController;     //배경 색상에 따른 블록 제어
    
    //초기화
    void Start()
    {
        //시작위치 초기화
        defPos = transform.position;
        //크기 초기화
        transform.localScale = new Vector3(0.8f, 0.8f, 1f); // 블록 크기 고정
    }

    void Update()
    {
        //배경이 노을일 때 블록 정지
        if (shiftController != null)
        {
            string back = shiftController.GetCurrentBackName();
            isStopped = (back == "back_sunset");
        }

        if (isStopped)
            return;

        // 이동 방향 반전
        if (transform.position.x < defPos.x - (range / 2))
        {
            direction = "right";
        }
        if (transform.position.x > defPos.x + (range / 2))
        {
            direction = "left";
        }

        // 이동
        Vector3 move = (direction == "right" ? Vector3.right : Vector3.left) * speed * Time.deltaTime;
        transform.position += move;

        // 플레이어도 함께 이동
        if (playerOnTop != null)
        {
            playerOnTop.transform.position += move;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnTop = collision.gameObject;

            // 플레이어 크기 강제 고정
            playerOnTop.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == playerOnTop)
        {
            playerOnTop = null;
        }
    }
}
