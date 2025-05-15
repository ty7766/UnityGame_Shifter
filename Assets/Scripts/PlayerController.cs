using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //�Է�
    Animator animator;      //�ִϸ����� �߰�

    public float speed = 3.0f;      //�̵� �ӵ�
    public float jump = 9.0f;       //���� ����
    public LayerMask groundLayer;   //���� ���� ���̾�
    
    bool goJump = false;            //�÷��̾ ���� ������
    bool onGround = false;          //�÷��̾ ���鿡 �ִ���

    //----------------- �ʱ�ȭ ---------------------
    void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal"); //���� �Է� Ȯ��

        if (axisH > 0.0f)
        {
            Debug.Log("������ �̵� ����");
            transform.localScale = new Vector3(0.8f, 0.8f, 0);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("���� �̵� ����");
            transform.localScale = new Vector3(-0.8f, 0.8f, 0);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //Animator �� ����
        animator.SetBool("isMove", axisH != 0);
        animator.SetBool("isJump", !onGround);
    }
    private void FixedUpdate()
    {
        //���� ����
        //�÷��̾�� ������ �����ϴ��� LineCast�� Ȯ��
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //�÷��̾ ���� ���� �ְų� �����̰� ���� ��
        if (onGround || axisH != 0)
        {
            //�ӵ� ����
            rbody.linearVelocity = new Vector2(speed * axisH, rbody.linearVelocity.y);
        }
        //���� ������ ����Ű�� ������ ��
        if(onGround && goJump)
        {
            Debug.Log("���� ��");
            Vector2 jumpPw = new Vector2(0, jump);       //���� ���� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); //���� ������ jmpPw��ŭ �߰� �̵�
            goJump = false;
        }
    }

    //------------------- �÷��̾� ���� ��ȣ�ۿ� (Ŭ����, ����)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
            Goal();
        else if (collision.gameObject.tag == "Dead")
            GameOver();
    }

    //------------------- ���� �ѱ� --------------------
    public void Jump()
    {
        goJump = true;
        Debug.Log("���� Ű ����");
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
