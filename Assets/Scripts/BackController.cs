using UnityEngine;

//배경 관리 로직
//ShiftController와 연결
public class BackController : MonoBehaviour
{
    public GameObject[] backGroundPrefab;   //배경 프리팹

    private int currentIndex = 0;           //현재 배경 인덱스
    private GameObject currentBackGround;

    void Start()
    {
        //첫번째 배경 생성
        SetBackGround(currentIndex);

        //배열에 잘 들어갔는지 디버깅 코드
        for (int i = 0; i < backGroundPrefab.Length; i++)
        {
            Debug.Log($"[{i}] {backGroundPrefab[i].name}");
        }
    }

    public void CycleBackGround()
    {
        //인덱스 순환
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
