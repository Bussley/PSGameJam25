using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    private GameObject sentence2;
    [SerializeField]
    private GameObject sentence3;

    private int clicks;

    public void Progress()
    {
        clicks++;
        if (clicks >= 3)
            SceneManager.LoadSceneAsync("MainScene");
        else if(clicks == 2)
            sentence3.SetActive(true);
        else if (clicks == 1)
            sentence2.SetActive(true);
    }
}
