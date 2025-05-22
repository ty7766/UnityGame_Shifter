using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shell;             //포탄 Prefab
    public float delayTime;              //발사 지연 시간
    public float fireSpeedX = -4.0f;     //발사 속도
    public float length = 8.0f;          //발사 거리

    GameObject player;                  //플레이어
    GameObject gate;                    //발사구
    float passedTimes = 0;              //경과 시간
    
    //초기화
    void Start()
    {
        Transform tr = transform.Find("gate");
        gate = tr.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;          //발사시간 판정

        if (CheckLength(player.transform.position))
        {
            if (passedTimes > delayTime)
            {
                passedTimes = 0;    //발사
                Vector3 pos = new Vector3(gate.transform.position.x,
                    gate.transform.position.y,
                    transform.position.z);

                //프리팹 Instantiate로 포탄 생성
                GameObject shellObj = Instantiate(shell, pos, Quaternion.identity);
                //발사 방향
                Rigidbody2D rbody = shellObj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, 0);
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //캐논 - 플레이어 간 거리 확인
    private bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);

        //설정한 거리 이내에 플레이어가 들어오면 활성화
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
