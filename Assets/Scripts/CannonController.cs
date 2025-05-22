using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shell;             //��ź Prefab
    public float delayTime;              //�߻� ���� �ð�
    public float fireSpeedX = -4.0f;     //�߻� �ӵ�
    public float length = 8.0f;          //�߻� �Ÿ�

    GameObject player;                  //�÷��̾�
    GameObject gate;                    //�߻籸
    float passedTimes = 0;              //��� �ð�
    
    //�ʱ�ȭ
    void Start()
    {
        Transform tr = transform.Find("gate");
        gate = tr.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;          //�߻�ð� ����

        if (CheckLength(player.transform.position))
        {
            if (passedTimes > delayTime)
            {
                passedTimes = 0;    //�߻�
                Vector3 pos = new Vector3(gate.transform.position.x,
                    gate.transform.position.y,
                    transform.position.z);

                //������ Instantiate�� ��ź ����
                GameObject shellObj = Instantiate(shell, pos, Quaternion.identity);
                //�߻� ����
                Rigidbody2D rbody = shellObj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, 0);
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    //ĳ�� - �÷��̾� �� �Ÿ� Ȯ��
    private bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);

        //������ �Ÿ� �̳��� �÷��̾ ������ Ȱ��ȭ
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
