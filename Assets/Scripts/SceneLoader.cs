using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNeedeLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
