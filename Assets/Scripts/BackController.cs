using UnityEngine;

public class BackController : MonoBehaviour
{
    public GameObject[] backGroundPrefab;   // ��� ������
    private int currentIndex = 0;           // ���� ��� �ε���
    private GameObject currentBackGround;

    void Start()
    {
        // ù ��° ��� �ٷ� ����
        SetBackGround(currentIndex, immediate: true);
    }

    public GameObject CycleBackGround(bool immediate = false)
    {
        currentIndex = (currentIndex + 1) % backGroundPrefab.Length;

        if (currentBackGround != null)
            Destroy(currentBackGround);

        return SetBackGround(currentIndex, immediate);
    }

    private GameObject SetBackGround(int index, bool immediate = false)
    {
        currentBackGround = Instantiate(backGroundPrefab[index], Vector3.zero, Quaternion.identity);
        currentBackGround.tag = "BackGround";

        if (immediate)
        {
            // ���� 1�� ��� ����
            var renderer = currentBackGround.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color c = renderer.material.color;
                c.a = 1f;
                renderer.material.color = c;
            }
        }

        return currentBackGround;
    }

    public string GetCurrentBackName()
    {
        return backGroundPrefab[currentIndex].name;
    }


}
