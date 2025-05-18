using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShiftController : MonoBehaviour
{
    [Header("쿨타임 관리")]
    public float maxCooldownTime = 2.0f;
    public TextMeshProUGUI textCooldownTime;
    public Image imageCooldownTime;

    [Header("이펙트 관리")]
    public float colorTime = 0.5f;
    public Image imageScreen;

    [Header("연결 스크립트")]
    public BackController backController;

    private float currentCooldownTime;
    private bool isCooldown;
    private Coroutine coolDownCoroutine;

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

    public void UseShift()
    {
        if (isCooldown)
            return;

        if (coolDownCoroutine != null)
            StopCoroutine(coolDownCoroutine);
        coolDownCoroutine = StartCoroutine(OnCooldownTime());

        //이펙트 실행
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        // 배경 전환 (다음 배경 반환받음)
        GameObject newBack = backController.CycleBackGround();

        // 색상 전환
        StartCoroutine(FadeSceneColor(newBack));
    }

    private IEnumerator OnCooldownTime()
    {
        currentCooldownTime = maxCooldownTime;
        SetCooldownIs(true);

        while (currentCooldownTime > 0.0f)
        {
            currentCooldownTime -= Time.deltaTime;
            imageCooldownTime.fillAmount = currentCooldownTime / maxCooldownTime;
            textCooldownTime.text = currentCooldownTime.ToString("F1");
            yield return null;
        }

        SetCooldownIs(false);
        coolDownCoroutine = null;
    }

    private void SetCooldownIs(bool b)
    {
        isCooldown = b;
        textCooldownTime.enabled = b;
        imageCooldownTime.enabled = b;
    }

    // 씬 내 모든 오브젝트 + 배경 색상 서서히 전환
    private IEnumerator FadeSceneColor(GameObject newBack)
    {
        // 현재 배경 이름 확인
        string backName = backController.GetCurrentBackName();

        Color targetColor = Color.white;

        if (backName.Contains("sunset"))
            targetColor = new Color(1f, 0.5f, 0f); // orange
        else if (backName.Contains("night"))
            targetColor = new Color(0.4f, 0f, 0.5f); // deep purple

        // Ground
        GameObject[] targetsGround = GameObject.FindGameObjectsWithTag("Ground");
        foreach (var obj in targetsGround)
            StartCoroutine(LerpColor(obj, targetColor, colorTime));

        // Enemy
        GameObject[] targetsEnemy = GameObject.FindGameObjectsWithTag("Dead");
        foreach (var obj in targetsEnemy)
            StartCoroutine(LerpColor(obj, targetColor, colorTime));

        yield return null;
    }

    private IEnumerator LerpColor(GameObject obj, Color targetColor, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
            yield break;

        Color startColor = renderer.material.color;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            renderer.material.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor;
    }

    //배경 전환 이펙트
    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;
        color.a = 0.2f;
        imageScreen.color = color;

        //투명도를 0까지 감소시키기
        while(color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}
