using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShiftController : MonoBehaviour
{
    [Header("��Ÿ�� ����")]
    public float maxCooldownTime = 2.0f;
    public TextMeshProUGUI textCooldownTime;
    public Image imageCooldownTime;

    [Header("����Ʈ ����")]
    public float colorTime = 0.5f;
    public Image imageScreen;

    [Header("���� ��ũ��Ʈ")]
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

        //����Ʈ ����
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        // ��� ��ȯ (���� ��� ��ȯ����)
        GameObject newBack = backController.CycleBackGround();

        // ���� ��ȯ
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

    // �� �� ��� ������Ʈ + ��� ���� ������ ��ȯ
    private IEnumerator FadeSceneColor(GameObject newBack)
    {
        // ���� ��� �̸� Ȯ��
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

    //��� ��ȯ ����Ʈ
    private IEnumerator HitAlphaAnimation()
    {
        Color color = imageScreen.color;
        color.a = 0.2f;
        imageScreen.color = color;

        //������ 0���� ���ҽ�Ű��
        while(color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}
