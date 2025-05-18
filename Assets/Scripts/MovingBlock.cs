using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;      //��� X�� �̵� �Ÿ�
    public float moveY = 0.0f;      //��� Y�� �̵� �Ÿ�
    public float times = 0.0f;      //�ð�
    public float weight = 0.0f;     //���� �ð�
    public bool isMoveWhenOn = false;   //�÷��̾ �ö��� �� �۵���ų������

    public bool isCanMove;          //������

    float perDX;                    //1�����Ӵ� ��� X�� �̵� ��
    float perDY;                    //1�����Ӵ� ��� Y�� �̵� ��

    Vector3 defPos;                 //��� �ʱ� ��ġ
    bool isReverse = false;         //���� ����


    //------------------- �ʱ�ȭ --------------------
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
        //�̵� ���̸� end ��Ȱ��ȭ�ϰ� ��� �ڱ� �ڽ��� ��ġ ������Ʈ
        if(isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if(isReverse)
            {
                //�̵����� + �̰� �̵� ��ġ�� �ʱ� ��ġ���� �۰ų� �̵����� - �̰� �̵���ġ�� �ʱ� ��ġ���� ũ��
                //�ݴ�������� �̵���Ű��
                if((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                    endX = true;

                if((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f &&  y >= defPos.y))
                    endY = true;

                //��� �̵���Ű��
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));        
            }
            else
            {
                //�̵����� +�̰� �̵� ��ġ�� �ʱ� ��ġ���� ũ�ų� �̵����� - �̰� �̵���ġ�� �ʱ���ġ���� ������
                //���������� �̵���Ű��
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                    endX = true;
                
                if ((perDY >= 0.0f && y >=defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                    endY = true;

                //��� �̵���Ű��
                Vector3 v = new Vector3(perDY, perDX, defPos.z);
                transform.Translate(v);
            }

            //�̵� ����� ��ġ ��ǥ�� �ٴٸ���
            if (endX &&  endY)
            {
                //��ġ ��߳� ����
                if (isReverse)
                    transform.position = defPos;
            }

            isReverse = !isReverse;     //�� ����
            isCanMove = false;          //�̵� ���� ���� false
            //�ö��� �� ������ ���� ���� ��� weight��ŭ ���� �� �ٽ� �̵�
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

    //--------------- �÷��̾� - ��� ���� -------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //���� �÷��̾ ž���ϸ� �̵������ �ڽ����� ����� �Բ� �̵���Ű��
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
                isCanMove = true;
        }
    }

    //----------------- �÷��̾� - ��� ���� ���� --------------------
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Playeer")
            collision.transform.SetParent(null);
    }
}
