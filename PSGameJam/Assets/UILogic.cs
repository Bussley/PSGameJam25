using TMPro;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    [SerializeField]
    private GameObject textValuePrefab;

    public void SpawnMoneyText(Vector3 pos, string text)
    {
        // Spawn Money
        GameObject money_text = Instantiate(textValuePrefab, Camera.main.WorldToScreenPoint(pos), Quaternion.identity);
        money_text.transform.position -= new Vector3(0.0f, 0.0f, 10.0f);
        money_text.transform.SetParent(transform, true);
        money_text.GetComponent<TextMeshProUGUI>().text = "+" + text;
    }
}
