using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;          //�� �̵� �ӵ�
    public string direction = "left";   //�� �̵� ����
    public float range = 0.0f;          //�� �̵� ����
    Vector3 defPos;                     //�� ���� ��ġ
    Vector3 enemyScale;                 //�� ũ��

    void Start()
    {
        //���� �ʱ�ȭ
        SetDirection(direction);
        //������ġ �ʱ�ȭ
        defPos = transform.position;
    }

    void Update()
    {
        // ���� ��� ������ 8�̸� ������ġ���� �������� 4��ŭ, ���������� 4��ŭ �̵��� �� ����
        // �� ��ġ�� range/2�� �Ѿ�� ���� ���� ���������Ƿ� �ݴ�������� ��ȯ��Ŵ
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

    //------------------- �� �ӵ� ���� ------------------------------
    void FixedUpdate()
    {
        //�ӵ� ����
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

    //----------------------- ���� - �� �浹 �˰��� ------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� ������ �浹�ϸ� �浹 �ݴ� �������� ���� ��ȯ
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

    //-------------- �� ���� ��ȯ --------------------
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
