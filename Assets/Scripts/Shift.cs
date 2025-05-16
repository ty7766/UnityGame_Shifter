using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shift : MonoBehaviour
{
    public float maxCooldownTime;                   //��ų ��Ÿ��
    public TextMeshProUGUI textCooldownTime;        //��ų ��Ÿ�� �ؽ�Ʈ
    public Image imageCooldownTime;                 //��ų ��Ÿ�� �̹���

    private float currentCooldownTime;              //���� ��ų ��Ÿ��
    private bool isCooldown;

    //��Ÿ�� �ʱ�ȭ
    private void Awake()
    {
        SetCooldownIs(false);
    }

    //��Ÿ�����̸� ��ư �Է� ����, ��Ÿ���� ������ ��ų ����
    public void UseShift()
    {
        if (isCooldown == true)
        {
            return;
        }

        // ��ų ������ (����)

        StartCoroutine(nameof(OnCooldownTime), maxCooldownTime);
    }

    //��ų�� ���Ǹ� max��Ÿ������ �ʱ�ȭ
    //time��ŭ ��ų ��Ÿ���� �پ��鼭 curr��Ÿ���� 0�̵Ǹ� ��ų �ٽ� Ȱ��ȭ
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
