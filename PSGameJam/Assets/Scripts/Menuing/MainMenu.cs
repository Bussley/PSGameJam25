using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject fadeImage;
	
    [SerializeField]
    private GameObject howToPlayScreen;
	
	[SerializeField]
	private AudioClip onClick;
	
	[SerializeField]
	private AudioClip onHover;
	
	[SerializeField]
	private AudioClip menuMusic;
	
	private AudioSource myaudio;
	private float countdown = 0.0f;
	private bool cutscene;


    private void Start()
    {
        howToPlayScreen.SetActive(false);
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
				else
					SceneManager.LoadSceneAsync("Credits");
			}
		}
	}

    public void GoToCutscene()
    {
		myaudio.PlayOneShot(onClick, 0.3f);
		countdown = 1.0f;
		cutscene = true;
        //SceneManager.LoadSceneAsync("Cutscene");
    }

    public void GoToCredits()
    {
		myaudio.PlayOneShot(onClick, 0.3f);
		countdown = 1.0f;
        //SceneManager.LoadSceneAsync("Credits");
    }

    public void SpawnHowToPlay()
    {
		myaudio.PlayOneShot(onClick, 0.3f);
        howToPlayScreen.SetActive(true);
    }

    public void QuitHowToPlay()
    {
		myaudio.PlayOneShot(onClick, 0.3f);
        howToPlayScreen.SetActive(false);
    }
	public void PlayHoverSound()
	{
		myaudio.PlayOneShot(onHover, 0.3f);
	}
}
