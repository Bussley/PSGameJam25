using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject fadeImage;
	
    [SerializeField]
    private GameObject howToPlayScreen;

    [SerializeField]
    private GameObject creditScreen;

    [SerializeField]
	private AudioClip onClick;
	
	[SerializeField]
	private AudioClip onHover;
	
	[SerializeField]
	private AudioClip menuMusic;
	
	private AudioSource myaudio;
	private float countdown = 0.0f;
	private bool cutscene;

	private bool interactMenuButtons;

    private void Start()
    {
		interactMenuButtons = true;
        howToPlayScreen.SetActive(false);
        creditScreen.SetActive(false);
        myaudio = GetComponent<AudioSource>();
    }
	
	private void FixedUpdate()
	{
		if(countdown > 0.0f)
		{
			countdown = countdown - Time.deltaTime;
			
			//Fading to black
			Color fade = fadeImage.GetComponent<UnityEngine.UI.Image>().color;
			fade.r = countdown;
			fade.b = countdown;
			fade.g = countdown;
			fadeImage.GetComponent<UnityEngine.UI.Image>().color = fade;
			if(countdown <= 0.0f)
			{
				if(cutscene)
					SceneManager.LoadSceneAsync("Cutscene");
			}
		}
	}

    public void GoToCutscene()
    {
		if (interactMenuButtons)
		{
			myaudio.PlayOneShot(onClick, 0.3f);
			countdown = 1.0f;
			cutscene = true;
		}
    }

    public void SpawnCredits()
    {
		if (interactMenuButtons)
        {
            interactMenuButtons = false;
            myaudio.PlayOneShot(onClick, 0.3f);
			creditScreen.SetActive(true);
		}
    }

    public void SpawnHowToPlay()
    {
		if (interactMenuButtons)
		{
			interactMenuButtons = false;
			myaudio.PlayOneShot(onClick, 0.3f);
			howToPlayScreen.SetActive(true);
		}
    }

    public void QuitHowToPlay()
    {
        interactMenuButtons = true;
        myaudio.PlayOneShot(onClick, 0.3f);
        howToPlayScreen.SetActive(false);
    }
	
    public void QuitCredits()
    {
        interactMenuButtons = true;
        myaudio.PlayOneShot(onClick, 0.3f);
        creditScreen.SetActive(false);
    }

    public void PlayHoverSound()
	{
		myaudio.PlayOneShot(onHover, 0.3f);
	}
}
