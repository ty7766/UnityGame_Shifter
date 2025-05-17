using UnityEngine;
using UnityEngine.SceneManagement;  //¾À °ü¸®

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    //¾À ºÒ·¯¿À±â
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
