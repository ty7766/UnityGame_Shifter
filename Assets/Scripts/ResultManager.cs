using UnityEngine;
using UnityEngine.UI;
using TMPro;

//���� ���� ȭ��
public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;

    //�ʱ�ȭ - �� ������ �ݿ�
    private void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.totalScore.ToString();
    }
}
