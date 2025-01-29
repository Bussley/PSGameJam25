using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlayScreen;


    private void Start()
    {
        howToPlayScreen.SetActive(false);
    }

    public void GoToCutscene()
    {
        SceneManager.LoadSceneAsync("Cutscene");
    }

    public void GoToCredits()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void SpawnHowToPlay()
    {
        howToPlayScreen.SetActive(true);
    }

    public void QuitHowToPlay()
    {
        howToPlayScreen.SetActive(false);
    }
}
