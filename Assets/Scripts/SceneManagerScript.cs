using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
	public Transform slider;
	public Transform progressText;
	public GameObject Continue;

	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

	public Image blackImage;
	public Animator fadingAnimator;
	public float fillWait;


	void Start ()
	{
		fadingAnimator.gameObject.SetActive (false);
        
	}

	void Update ()
	{
		if (currentAmount < 100) {
			currentAmount += speed * Time.deltaTime;
			progressText.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
          
		} else {
			Continue.SetActive (true);

			slider.gameObject.SetActive (false);

		}

		slider.GetComponent<Slider> ().value = currentAmount / 100;
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}

	/* public void GoToPlayScene(string scene)
    {

        SceneManager.LoadScene(scene);
    }*/
	
	public void SwitchScene ()
	{
		//Continue.gameObject.SetActive(false);
		StartCoroutine (FadingScene ());
		GameObject spash = GameObject.Find ("CustomSplashScreen");
		spash.GetComponent<SplashScreenScript> ().FadeIn ();
		//fadingAnimator.gameObject.SetActive(true);
		//fadingAnimator.Play("FadeIn");
	}

	IEnumerator FadingScene ()
	{
        
		yield return new WaitForSeconds (2f);
       
		//yield return new WaitUntil(() => blackImage.color.a == 1);
		SceneManager.LoadScene (0);
	}


	/* public void LoadLevel(string sceneIndex)
     {
         StartCoroutine(LoadAsynhronuously(sceneIndex));
     }
     */



}

/*	IEnumerator LoadAsynhronuously(string sceneIndex)
            {

                AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

                loadingScreen.SetActive (true);

                while (!operation.isDone) 
                {
                    float progress = Mathf.Clamp01 (operation.progress / 0.9f);
                    slider.value = progress;
                    progressText.text = progress * 100f + "%";

                    yield return null;
                }
            }*/
    