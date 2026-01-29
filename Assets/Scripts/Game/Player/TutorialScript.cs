using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] string nameScene;
    [SerializeField] AudioSource audioSource;
    public void Start()
    {
        StartCoroutine(DelayBeforeStart());
    }

    IEnumerator DelayBeforeStart(float cutSceneDuration = 2f)
    {
        yield return new WaitForSeconds(4.5f + cutSceneDuration);
        // MouseController.instance.controll = true;
        SceneManager.UnloadSceneAsync(nameScene);
    }

    public void PlaySfx(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }


}
