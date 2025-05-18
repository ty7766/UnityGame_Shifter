using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;      //블록 X축 이동 거리
    public float moveY = 0.0f;      //블록 Y축 이동 거리
    public float times = 0.0f;      //시간
    public float weight = 0.0f;     //정지 시간
    public bool isMoveWhenOn = false;   //플레이어가 올라갔을 때 작동시킬것인지

    public bool isCanMove;          //움직임

    float perDX;                    //1프레임당 블록 X축 이동 값
    float perDY;                    //1프레임당 블록 Y축 이동 값

    Vector3 defPos;                 //블록 초기 위치
    bool isReverse = false;         //반전 여부


    //------------------- 초기화 --------------------
    private void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times);
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
            isCanMove = true;
    }

    private void FixedUpdate()
    {
        //이동 중이면 end 비활성화하고 계속 자기 자신의 위치 업데이트
        if(isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if(isReverse)
            {
                //이동량이 + 이고 이동 위치가 초기 위치보다 작거나 이동량이 - 이고 이동위치가 초기 위치보다 크면
                //반대방향으로 이동시키기
                if((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                    endX = true;

                if((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f &&  y >= defPos.y))
                    endY = true;

                //블록 이동시키기
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));        
            }
            else
            {
                //이동량이 +이고 이동 위치가 초기 위치보다 크거나 이동량이 - 이고 이동위치가 초기위치보다 작으면
                //정방향으로 이동시키기
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                    endX = true;
                
                if ((perDY >= 0.0f && y >=defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                    endY = true;

                //블록 이동시키기
                Vector3 v = new Vector3(perDY, perDX, defPos.z);
                transform.Translate(v);
            }

            //이동 블록이 위치 목표에 다다르면
            if (endX &&  endY)
            {
                //위치 어긋남 방지
                if (isReverse)
                    transform.position = defPos;
            }

            isReverse = !isReverse;     //값 반전
            isCanMove = false;          //이동 가능 값을 false
            //올라갔을 때 움직임 값이 꺼진 경우 weight만큼 지연 후 다시 이동
            if (isMoveWhenOn == false)
                Invoke("Move", weight);
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    //--------------- 플레이어 - 블록 접촉 -------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //만약 플레이어가 탑승하면 이동블록의 자식으로 만들어 함께 이동시키기
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
                isCanMove = true;
        }
    }

    //----------------- 플레이어 - 블록 접촉 해제 --------------------
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Playeer")
            collision.transform.SetParent(null);
    }
}
