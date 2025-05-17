using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//배경 전환 버튼 관리와 쿨타임 관리
//BackController와 연결
public class ShiftController : MonoBehaviour
{
    [Header("쿨타임 관리")]
    public float maxCooldownTime = 2.0f;                   //스킬 쿨타임
    public TextMeshProUGUI textCooldownTime;        //스킬 쿨타임 텍스트
    public Image imageCooldownTime;                 //스킬 쿨타임 이미지

    [Header("이펙트 관리")]
    public float slideTime = 0.3f;
    public float colorTime = 0.3f;

    [Header("연결 스크립트")]
    public BackController backController;

    private float currentCooldownTime;              //현재 스킬 쿨타임
    private bool isCooldown;

    //쿨타임 초기화
    private void Awake()
    {
        SetCooldownIs(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseShift();
        }
    }

    //쿨타임중이면 버튼 입력 무시, 쿨타임이 없으면 스킬 실행
    public void UseShift()
    {
        if (isCooldown)
            return;

        //쿨타임 시작
        if (coolDownCoroutine != null)
            StopCoroutine(coolDownCoroutine);
        coolDownCoroutine = StartCoroutine(OnCooldownTime());

        //배경 전환
        StartCoroutine(SlideTransition());

        //지형, 몹 색상 변화
        StartCoroutine(FadeColorTo());
    }

    //스킬이 사용되면 max쿨타임으로 초기화
    //time만큼 스킬 쿨타임이 줄어들면서 curr쿨타임이 0이되면 스킬 다시 활성화
    private IEnumerator OnCooldownTime(float maxCooldownTime)
    {
        currentCooldownTime = maxCooldownTime;

        SetCooldownIs(true);

        while ( currentCooldownTime > 0.0f)
        {
            currentCooldownTime -= Time.deltaTime;
            imageCooldownTime.fillAmount = currentCooldownTime / maxCooldownTime;
            textCooldownTime.text = currentCooldownTime.ToString("F1");

            yield return null;
        }

        SetCooldownIs(false);
    }

    //슬라이드 배경 전환 이펙트
    private IEnumerator SlideTransition()
    {
        //배경 태그를 찾아서 전환
        GameObject prev = GameObject.FindWithTag("Background");
        backController.CycleBackGround();

        GameObject next = GameObject.FindWithTag("Background");
        if (next == null)
            yield break;

        //이전 배경 시작 위치 그 자리 그대로 다음 배경 위치 생성
        Vector3 startPos = new Vector3(-20, 0, 0);
        Vector3 endPos = Vector3.zero;
        next.transform.position = startPos;

        float elapsed = 0.0f;

        //다음 배경이 이전 배경을 다 덮을 때까지 slideTime만큼 실행
        while (elapsed < slideTime)
        {
            next.transform.position = Vector3.Lerp(startPos, endPos, elapsed / slideTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //다 덮으면 그 자리에 endPos 생성
        next.transform.position = endPos;

        //다 덮으면 이전 배경 제거
        if (prev != null)
            Destroy(prev);
    }

    private void SetCooldownIs(bool b)
    {
        isCooldown = b;
        textCooldownTime.enabled = b;
        imageCooldownTime.enabled = b;
    }
}
