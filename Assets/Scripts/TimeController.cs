using UnityEngine;

public class TimeController : MonoBehaviour
{

    public bool isCountDown = true;     //카운트다운, 카운트업 선택
    public float maxTime = 0;
    public bool isTimeOver = false;     //타임 오버가 되었는지 확인
    public float displayTime = 0;       // 표시 시간

    private float currentTimes = 0;            //현재 시간

    //초기화
    void Start()
    {
        //현재 표시되는 시간을 최대 시간으로 설정
        if (isCountDown)
            displayTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        //카운트 다운 알고리즘
        if (isTimeOver == false)
        {
            currentTimes += Time.deltaTime;

            //최대시간에서 현재 지나간 시간을 빼서 표시
            if (isCountDown)
            {
                displayTime = maxTime - currentTimes;

                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f; //보정
                    isTimeOver = true;
                }
            }
            //카운트 업 알고리즘
            else
            {
                displayTime = currentTimes;

                //현재 지나간 시간 표시
                if (displayTime >= maxTime)
                {
                    displayTime = maxTime;  //보정
                    isTimeOver = true;
                }
            }
            Debug.Log("TIME : " + displayTime);     //디버깅 출력
        }
    }
}
