using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingUI : MonoBehaviour
{
    [SerializeField]   AudioSource audioSource;
    public void LoadCreditsScene()
    {
        StartCoroutine(LoadSceneWithTransition("Credit"));
    }

     IEnumerator LoadSceneWithTransition(string sceneName)
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }


    public void PlaySfx(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    
}
