using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;     //제거 시간

    private void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    //무언가에 접촉하면 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
