using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
