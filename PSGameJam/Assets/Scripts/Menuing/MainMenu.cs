using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToCutscene()
    {
        SceneManager.LoadSceneAsync("Cutscene");
    }

    public void GoToCredits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
