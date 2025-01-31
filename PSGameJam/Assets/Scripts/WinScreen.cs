using UnityEngine;
using UnityEngine.InputSystem;

public class WinScreen : MonoBehaviour
{
    private GameObject winScreen;
    private bool haswon;

    private void Awake()
    {
        winScreen = GameObject.FindGameObjectWithTag("WinScreen");
        winScreen.SetActive(false);
        haswon = false;
    }

    public void CloseWinScreen(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed && haswon)
        {
            winScreen.SetActive(false);
        }
    }

    public void Win()
    {
        if (!haswon)
        {
            haswon = true;
            winScreen.SetActive(true);
        }
    }
}
