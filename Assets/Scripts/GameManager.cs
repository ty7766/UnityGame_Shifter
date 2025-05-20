using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GameOver/Clear UI")]
    public GameObject mainImage;        //�̹����� ��Ƶ�
    public Sprite gameOverSpr;          //GAMEOVER�̹���
    public Sprite gameClearSpr;         //GAMECLEAR �̹���
    public GameObject panel;            // �г�
    public GameObject restartButton;    //RESTART ��ư
    public GameObject nextButton;       //NEXT ��ư

    [Header("Time UI")]
    public GameObject timeBar;          //�ð� ǥ�� �̹���
    public GameObject timeText;         //�ð� ǥ�� �ؽ�Ʈ
    TimeController timeController;

    [Header("Score UI")]
    public GameObject scoreText;        //���� ǥ�� �ؽ�Ʈ
    public static int totalScore;       //���� ����(static)
    public int stageScore;              //�������� ����

    private Image titleImage;                   //Title �̹���

    void Start()
    {
        //UI Controll
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        //Time Controll
        timeController = GetComponent<TimeController>();
        //�ð� ������ ���� ��� Time UI ����
        if (timeController != null)
            if (timeController.maxTime == 0.0f)
                timeBar.SetActive(false);

        //Score Controll
        UpdateScore();
    }

    //���� Ŭ������ ��� Ŭ���� UI ������, ���� ������Ʈ, �ð� �ʱ�ȭ
    //���� ������ ��� Ŭ���� UI ������, ���� ������Ʈ, �ð� �ʱ�ȭ
    //�÷��̾� �� ���¸� �߰��Ͽ� ���¿� ���� �޼ҵ� ����
    void Update()
    {
        //�÷��̾ ������ Ŭ������ ���
        if (PlayerController.gameState == "gameclear")
        {
            mainImage.SetActive(true);      //UI ���̱�
            panel.SetActive(true);          //��ư ���̱�

            //RESTART ��ȿȭ?

            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";

            //�ð� ���߱� & ���� �ð� ���� ��
            if (timeController != null)
            {
                timeController.isTimeOver = true;

                //�������� ���� ����(�ð�)
                int time = (int)timeController.displayTime;
                totalScore += time * 10;
            }

            //�������� ���� ����(����)
            totalScore += stageScore;       //�� �����ӿ��� ���� ����
            stageScore = 0;                 //���� �����ӿ� stageScore = 0���� ����� ������ �ߺ� �������� �ʰ� ��
            UpdateScore();
        }

        //�÷��̾ ���� ���� �� ���
        else if (PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);      //UI ǥ��
            panel.SetActive(true);          //��ư ���̱�

            //NEXT ��Ȱ��
            Button button = nextButton.GetComponent<Button>();
            button.interactable = false;

            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";

            //TimeControll
            if (timeController != null)
                timeController.isTimeOver = true;
        }

        //���� ���� ���
        else if (PlayerController.gameState == "playing") 
        {
            //TimeController���� �������� ī��Ʈ�� �ؽ�Ʈ�� ǥ��
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();

            //�ð��ؽ�Ʈ ����
            if (timeController != null)
                if (timeController.maxTime > 0.0f)
                {
                    //�������� time�� ������ �ؽ�Ʈ�� ���
                    int time = (int)timeController.displayTime;
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();

                    //���� time�� 0�̸� ���� ���� ���·� ����
                    if (time <= 0)
                        playerController.GameOver();
                }

            //���� �߰�
            if (playerController.score != 0)
            {
                stageScore += playerController.score;
                playerController.score = 0;
                UpdateScore();
            }
        }

    }

    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //UI�� ���� ����
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
