using UnityEngine;

//��� ���� ����
//ShiftController�� ����
public class BackController : MonoBehaviour
{
    public GameObject[] backGroundPrefab;   //��� ������

    private int currentIndex = 0;           //���� ��� �ε���
    private GameObject currentBackGround;

    void Start()
    {
        //ù��° ��� ����
        SetBackGround(currentIndex);

        //�迭�� �� ������ ����� �ڵ�
        for (int i = 0; i < backGroundPrefab.Length; i++)
        {
            Debug.Log($"[{i}] {backGroundPrefab[i].name}");
        }
    }

    public void CycleBackGround()
    {
        //�ε��� ��ȯ
        currentIndex = (currentIndex + 1) % backGroundPrefab.Length;

        if (currentBackGround != null)
            Destroy(currentBackGround);

        SetBackGround(currentIndex);
    }
    private void SetBackGround(int index)
    {
        currentBackGround = Instantiate(backGroundPrefab[index], Vector3.zero, Quaternion.identity);
    }
}
