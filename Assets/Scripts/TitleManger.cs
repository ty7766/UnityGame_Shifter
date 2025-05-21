using UnityEngine;

public class TitleManger : MonoBehaviour
{
    [Header("Title UI")]
    public GameObject tutorialImage;    //���� ��� �̹���
    public GameObject tutorialButton;   //���� ��� ��ư
    public GameObject exitButton;       //���� ������ ��ư
    bool isTutorialActive = false;

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ Ʃ�丮��ȭ�鿡�� ESC�� ������ �� �ݱ�
        if (isTutorialActive && Input.GetKeyDown(KeyCode.Escape))
            CloseTutorialImage();
    }

    //"���� ���" ��ư�� ������ �� �̹��� �����ֱ�
    public void ShowTutorialImage()
    {
        Debug.Log("Ʃ�丮�� ����");
        if (tutorialImage != null)
        {
            tutorialImage.SetActive(true);
            isTutorialActive = true;
        }
    }

    //"���� ���" ȭ�鿡�� ESC ������ ������
    void CloseTutorialImage()
    {
        if (tutorialImage != null)
        {
            tutorialImage.SetActive(false);
            isTutorialActive = false;
        }
    }

    //"���� ����" ��ư�� ������ �� ������
    public void ExitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
