using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//��� ��ȯ ��ư ������ ��Ÿ�� ����
//BackController�� ����
public class ShiftController : MonoBehaviour
{
    [Header("��Ÿ�� ����")]
    public float maxCooldownTime = 2.0f;                   //��ų ��Ÿ��
    public TextMeshProUGUI textCooldownTime;        //��ų ��Ÿ�� �ؽ�Ʈ
    public Image imageCooldownTime;                 //��ų ��Ÿ�� �̹���

    [Header("����Ʈ ����")]
    public float slideTime = 0.3f;
    public float colorTime = 0.3f;

    [Header("���� ��ũ��Ʈ")]
    public BackController backController;

    private float currentCooldownTime;              //���� ��ų ��Ÿ��
    private bool isCooldown;

    //��Ÿ�� �ʱ�ȭ
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

    //��Ÿ�����̸� ��ư �Է� ����, ��Ÿ���� ������ ��ų ����
    public void UseShift()
    {
        if (isCooldown)
            return;

        //��Ÿ�� ����
        if (coolDownCoroutine != null)
            StopCoroutine(coolDownCoroutine);
        coolDownCoroutine = StartCoroutine(OnCooldownTime());

        //��� ��ȯ
        StartCoroutine(SlideTransition());

        //����, �� ���� ��ȭ
        StartCoroutine(FadeColorTo());
    }

    //��ų�� ���Ǹ� max��Ÿ������ �ʱ�ȭ
    //time��ŭ ��ų ��Ÿ���� �پ��鼭 curr��Ÿ���� 0�̵Ǹ� ��ų �ٽ� Ȱ��ȭ
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

    //�����̵� ��� ��ȯ ����Ʈ
    private IEnumerator SlideTransition()
    {
        //��� �±׸� ã�Ƽ� ��ȯ
        GameObject prev = GameObject.FindWithTag("Background");
        backController.CycleBackGround();

        GameObject next = GameObject.FindWithTag("Background");
        if (next == null)
            yield break;

        //���� ��� ���� ��ġ �� �ڸ� �״�� ���� ��� ��ġ ����
        Vector3 startPos = new Vector3(-20, 0, 0);
        Vector3 endPos = Vector3.zero;
        next.transform.position = startPos;

        float elapsed = 0.0f;

        //���� ����� ���� ����� �� ���� ������ slideTime��ŭ ����
        while (elapsed < slideTime)
        {
            next.transform.position = Vector3.Lerp(startPos, endPos, elapsed / slideTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //�� ������ �� �ڸ��� endPos ����
        next.transform.position = endPos;

        //�� ������ ���� ��� ����
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
