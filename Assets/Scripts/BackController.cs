using UnityEngine;

public class BackController : MonoBehaviour
{
    public GameObject[] backGroundPrefab;   // 배경 프리팹
    private int currentIndex = 0;           // 현재 배경 인덱스
    private GameObject currentBackGround;

    void Start()
    {
        // 첫 번째 배경 바로 생성
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
            // 투명도 1로 즉시 설정
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
