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

    private Image titleImage;                   //Title �̹���

    void Start()
    {
        //UI �����
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        //Time Controll
        timeController = GetComponent<TimeController>();
        //�ð� ������ ���� ��� Time UI ����
        if (timeController != null)
            if (timeController.maxTime == 0.0f)
                timeBar.SetActive(false);
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

            //TimeControll
            if (timeController != null)
                timeController.isTimeOver = true;
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
        }

    }

    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
