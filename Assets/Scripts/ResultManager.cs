using UnityEngine;
using UnityEngine.UI;
using TMPro;

//게임 종료 화면
public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;

    //초기화 - 총 점수를 반영
    private void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.totalScore.ToString();
    }
}
