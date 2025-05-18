using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;        //�̹����� ��Ƶ�
    public Sprite gameOverSpr;          //GAMEOVER�̹���
    public Sprite gameClearSpr;         //GAMECLEAR �̹���
    public GameObject panel;            // �г�
    public GameObject restartButton;    //RESTART ��ư
    public GameObject nextButton;       //NEXT ��ư

    Image titleImage;                   //Title �̹���

    void Start()
    {
        //UI �����
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
    }

    // Update is called once per frame
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
        }

        //���� ���� ���
        else if (PlayerController.gameState == "playing") 
        {
            //
        }
    }

    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
