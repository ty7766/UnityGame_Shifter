using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f;     //���� �ð�

    private void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    //���𰡿� �����ϸ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
