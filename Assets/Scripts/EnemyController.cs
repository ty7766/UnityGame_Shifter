using UnityEngine;


//���絵 �ִϸ��̼��� �۵��ϴ� ����
//�ӵ��� �ٷ� �������
//���� �Ǿ��� �� Enemy�� �±װ� Ground�� �ٲ�� ��������...
public class EnemyController : MonoBehaviour
{
    [Header("�� ����")]
    public float speed = 2.0f;          //�� �̵� �ӵ�
    public string direction = "left";   //�� �̵� ����
    public float range = 0.0f;          //�� �̵� ����
    public bool isStopped = false;      //���� �̵� ����
    Vector3 defPos;                     //�� ���� ��ġ

    [Header("���� ��ũ��Ʈ")]
    public ShiftController shiftController;     //��� ���� ���� �� ����

    void Start()
    {
        //���� �ʱ�ȭ
        SetDirection(direction);
        //������ġ �ʱ�ȭ
        defPos = transform.position;
    }

    void Update()
    {
        //����� ���� �� �� ������Ʈ ����
        if (shiftController != null)
        {
            string back = shiftController.GetCurrentBackName();
            isStopped = (back == "back_night");
        }
        // ���� ��� ������ 8�̸� ������ġ���� �������� 4��ŭ, ���������� 4��ŭ �̵��� �� ����
        // �� ��ġ�� range/2�� �Ѿ�� ���� ���� ���������Ƿ� �ݴ�������� ��ȯ��Ŵ
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

    //------------------- �� �ӵ� ���� -----------------------
    void FixedUpdate()
    {
        //����� night�϶� �� ����
        if (isStopped) return;

        //�ӵ� ����
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        if (direction == "left")
        {
            rbody.linearVelocity = new Vector2(-speed,rbody.linearVelocity.y);
        }
        else
        {
            rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
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
