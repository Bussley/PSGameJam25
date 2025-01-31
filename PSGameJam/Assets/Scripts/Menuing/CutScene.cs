using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI sentence1;
    [SerializeField]
    private TextMeshProUGUI sentence2;
    [SerializeField]
    private TextMeshProUGUI sentence3;

    [SerializeField] 
    private Image image1;
    [SerializeField]
    private Image image2;
    [SerializeField]
    private Image image3;

    private int clicks;
    private float waitTime = 1.0f;
    private float waitButtonInteract;

    private void Start()
    {
        waitButtonInteract = Time.time + waitTime;
        StartCoroutine(FadeTextToFullAlpha(waitTime, sentence1));
    }

    public void Progress()
    {
        if (waitButtonInteract < Time.time)
        {
            clicks++;
            if (clicks >= 3)
                SceneManager.LoadSceneAsync("MainScene");
            else if (clicks == 2)
            {
                waitButtonInteract = Time.time + waitTime;
                StartCoroutine(FadeImageToFullAlpha(waitTime, image3));
                StartCoroutine(FadeImageToZeroAlpha(waitTime, image2));
                StartCoroutine(FadeTextToZeroAlpha(waitTime, sentence2));
                StartCoroutine(FadeTextToFullAlpha(waitTime, sentence3));
            }
            else if (clicks == 1)
            {
                waitButtonInteract = Time.time + waitTime;
                StartCoroutine(FadeImageToFullAlpha(waitTime, image2));
                StartCoroutine(FadeImageToZeroAlpha(waitTime, image1));
                StartCoroutine(FadeTextToZeroAlpha(waitTime, sentence1));
                StartCoroutine(FadeTextToFullAlpha(waitTime, sentence2));
            }
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }


}
