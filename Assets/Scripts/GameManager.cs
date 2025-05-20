using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GameOver/Clear UI")]
    public GameObject mainImage;        //이미지를 담아둠
    public Sprite gameOverSpr;          //GAMEOVER이미지
    public Sprite gameClearSpr;         //GAMECLEAR 이미지
    public GameObject panel;            // 패널
    public GameObject restartButton;    //RESTART 버튼
    public GameObject nextButton;       //NEXT 버튼

    [Header("Time UI")]
    public GameObject timeBar;          //시간 표시 이미지
    public GameObject timeText;         //시간 표시 텍스트
    TimeController timeController;

    [Header("Score UI")]
    public GameObject scoreText;        //점수 표시 텍스트
    public static int totalScore;       //총합 점수(static)
    public int stageScore;              //스테이지 점수

    private Image titleImage;                   //Title 이미지

    void Start()
    {
        //UI Controll
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        //Time Controll
        timeController = GetComponent<TimeController>();
        //시간 제한이 없는 경우 Time UI 숨김
        if (timeController != null)
            if (timeController.maxTime == 0.0f)
                timeBar.SetActive(false);

        //Score Controll
        UpdateScore();
    }

    //게임 클리어한 경우 클리어 UI 보여줌, 상태 업데이트, 시간 초기화
    //게임 오버한 경우 클리어 UI 보여줌, 상태 업데이트, 시간 초기화
    //플레이어 각 상태를 추가하여 상태에 따른 메소드 적용
    void Update()
    {
        //플레이어가 게임을 클리어한 경우
        if (PlayerController.gameState == "gameclear")
        {
            mainImage.SetActive(true);      //UI 보이기
            panel.SetActive(true);          //버튼 보이기

            //RESTART 무효화?

            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";

            //시간 멈추기 & 남은 시간 점수 합
            if (timeController != null)
            {
                timeController.isTimeOver = true;

                //스테이지 점수 갱신(시간)
                int time = (int)timeController.displayTime;
                totalScore += time * 10;
            }

            //스테이지 점수 갱신(보석)
            totalScore += stageScore;       //한 프레임에만 점수 갱신
            stageScore = 0;                 //다음 프레임에 stageScore = 0으로 만들어 점수가 중복 누적되지 않게 함
            UpdateScore();
        }

        //플레이어가 게임 오버 한 경우
        else if (PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);      //UI 표시
            panel.SetActive(true);          //버튼 보이기

            //NEXT 비활성
            Button button = nextButton.GetComponent<Button>();
            button.interactable = false;

            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";

            //TimeControll
            if (timeController != null)
                timeController.isTimeOver = true;
        }

        //게임 중인 경우
        else if (PlayerController.gameState == "playing") 
        {
            //TimeController에서 진행중인 카운트를 텍스트에 표시
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();

            //시간텍스트 갱신
            if (timeController != null)
                if (timeController.maxTime > 0.0f)
                {
                    //진행중인 time을 가져와 텍스트로 출력
                    int time = (int)timeController.displayTime;
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();

                    //만약 time이 0이면 게임 오버 상태로 변경
                    if (time <= 0)
                        playerController.GameOver();
                }

            //점수 추가
            if (playerController.score != 0)
            {
                stageScore += playerController.score;
                playerController.score = 0;
                UpdateScore();
            }
        }

    }

    //이미지 숨기기
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //UI에 점수 갱신
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
