using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�� �Ӽ�")]
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
        //����� ������ �� �� ������Ʈ ����
        if (shiftController != null)
        {
            string back = shiftController.GetCurrentBackName();
            isStopped = (back == "back_sunset");
        }

        //����� ������ �� �±׸� Ground�� �ٲپ� �÷��̾ ��Ƶ� Dead ������ �ȵǰ� ����
        if (isStopped && tag != "Ground")
            tag = "Ground";
        else if (!isStopped && tag != "Dead")
            tag = "Dead";

        //����� ������ �� �ִϸ��̼� ����
        Animator animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("IsMoving", !isStopped);


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

        //�ӵ� ����
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        //����� sunset�϶� �� ��� ���߱�
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
