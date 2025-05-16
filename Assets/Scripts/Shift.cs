using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shift : MonoBehaviour
{
    public float maxCooldownTime;                   //스킬 쿨타임
    public TextMeshProUGUI textCooldownTime;        //스킬 쿨타임 텍스트
    public Image imageCooldownTime;                 //스킬 쿨타임 이미지

    private float currentCooldownTime;              //현재 스킬 쿨타임
    private bool isCooldown;

    //쿨타임 초기화
    private void Awake()
    {
        SetCooldownIs(false);
    }

    //쿨타임중이면 버튼 입력 무시, 쿨타임이 없으면 스킬 실행
    public void UseShift()
    {
        if (isCooldown == true)
        {
            return;
        }

        // 스킬 구현부 (미정)

        StartCoroutine(nameof(OnCooldownTime), maxCooldownTime);
    }

    //스킬이 사용되면 max쿨타임으로 초기화
    //time만큼 스킬 쿨타임이 줄어들면서 curr쿨타임이 0이되면 스킬 다시 활성화
    private IEnumerator OnCooldownTime(float maxCooldownTime)
    {
        currentCooldownTime = maxCooldownTime;

        SetCooldownIs(true);

        while ( currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            imageCooldownTime.fillAmount = currentCooldownTime / maxCooldownTime;
            textCooldownTime.text = currentCooldownTime.ToString("F1");

            yield return null;
        }

        SetCooldownIs(false);
    }

    private void SetCooldownIs(bool b)
    {
        isCooldown = b;
        textCooldownTime.enabled = b;
        imageCooldownTime.enabled = b;
    }
}
