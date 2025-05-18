using UnityEngine;

public class TimeController : MonoBehaviour
{

    public bool isCountDown = true;     //ī��Ʈ�ٿ�, ī��Ʈ�� ����
    public float maxTime = 0;
    public bool isTimeOver = false;     //Ÿ�� ������ �Ǿ����� Ȯ��
    public float displayTime = 0;       // ǥ�� �ð�

    private float currentTimes = 0;            //���� �ð�

    //�ʱ�ȭ
    void Start()
    {
        //���� ǥ�õǴ� �ð��� �ִ� �ð����� ����
        if (isCountDown)
            displayTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        //ī��Ʈ �ٿ� �˰���
        if (isTimeOver == false)
        {
            currentTimes += Time.deltaTime;

            //�ִ�ð����� ���� ������ �ð��� ���� ǥ��
            if (isCountDown)
            {
                displayTime = maxTime - currentTimes;

                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f; //����
                    isTimeOver = true;
                }
            }
            //ī��Ʈ �� �˰���
            else
            {
                displayTime = currentTimes;

                //���� ������ �ð� ǥ��
                if (displayTime >= maxTime)
                {
                    displayTime = maxTime;  //����
                    isTimeOver = true;
                }
            }
            Debug.Log("TIME : " + displayTime);     //����� ���
        }
    }
}
