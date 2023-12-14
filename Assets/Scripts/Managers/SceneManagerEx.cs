using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get; set; }

    public void LoadScene(string sceneName)
    {
        Managers.Clear();
        SceneManager.LoadScene(sceneName);
    }
}