using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;        //이미지를 담아둠
    public Sprite gameOverSpr;          //GAMEOVER이미지
    public Sprite gameClearSpr;         //GAMECLEAR 이미지
    public GameObject panel;            // 패널
    public GameObject restartButton;    //RESTART 버튼
    public GameObject nextButton;       //NEXT 버튼

    Image titleImage;                   //Title 이미지

    void Start()
    {
        //UI 숨기기
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
    }

    // Update is called once per frame
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
        }

        //게임 중인 경우
        else if (PlayerController.gameState == "playing") 
        {
            //
        }
    }

    //이미지 숨기기
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
