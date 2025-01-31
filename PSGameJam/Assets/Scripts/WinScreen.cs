using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    private GameObject winScreen;
    private GameObject winText;
    private bool haswon;

    private void Awake()
    {
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
        winText = GameObject.FindGameObjectWithTag("WinText");
        winScreen.SetActive(false);
        haswon = false;
    }

    private void Update()
    {
        if(haswon && Input.GetKeyDown(KeyCode.F))
        {
            winScreen.SetActive(false);
        }
    }

    public void Win()
    {
        if (!haswon)
        {
            winScreen.SetActive(true);
            StartCoroutine(FadeImageToFullAlpha(1.0f));
            StartCoroutine(FadeTextToFullAlpha(30.0f));
            haswon = true;
        }
    }

    public IEnumerator FadeImageToFullAlpha(float t)
    {
        Image i = winScreen.GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t)
    {
        var i = winText.GetComponent<TextMeshProUGUI>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
