using UnityEngine;
using UnityEngine.SceneManagement;  //�� ����

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    //�� �ҷ�����
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
