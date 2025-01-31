using System.Collections;
using TMPro;
using UnityEngine;

public class DestroyMoneyText : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(FadeTextToZeroAlpha(0.5f));
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        var i = gameObject.GetComponent<TextMeshProUGUI>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        Destroy(gameObject);
    }
}
