using UnityEngine;

public class TitleManger : MonoBehaviour
{
    [Header("Title UI")]
    public GameObject tutorialImage;    //게임 방법 이미지
    public GameObject tutorialButton;   //게임 방법 버튼
    public GameObject exitButton;       //게임 나가기 버튼
    bool isTutorialActive = false;

    // Update is called once per frame
    void Update()
    {
        //플레이어가 튜토리얼화면에서 ESC를 눌렀을 때 닫기
        if (isTutorialActive && Input.GetKeyDown(KeyCode.Escape))
            CloseTutorialImage();
    }

    //"게임 방법" 버튼을 눌렀을 때 이미지 보여주기
    public void ShowTutorialImage()
    {
        Debug.Log("튜토리얼 열림");
        if (tutorialImage != null)
        {
            tutorialImage.SetActive(true);
            isTutorialActive = true;
        }
    }

    //"게임 방법" 화면에서 ESC 누르면 나가기
    void CloseTutorialImage()
    {
        if (tutorialImage != null)
        {
            tutorialImage.SetActive(false);
            isTutorialActive = false;
        }
    }

    //"게임 종료" 버튼을 눌렀을 때 나가기
    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
