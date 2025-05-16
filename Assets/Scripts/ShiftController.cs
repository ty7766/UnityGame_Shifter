using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;

//��� ��ȯ ��ư ������ ��Ÿ�� ����
//BackController�� ����
public class ShiftController : MonoBehaviour
{
    public float maxCooldownTime = 5.0f;                   //��ų ��Ÿ��
    public TextMeshProUGUI textCooldownTime;        //��ų ��Ÿ�� �ؽ�Ʈ
    public Image imageCooldownTime;                 //��ų ��Ÿ�� �̹���

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
        if (isCooldown == true)
        {
            return;
        }
        if (backController !=  null)
        {
            backController.CycleBackGround();
        }

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
