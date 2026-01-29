using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    [Header("Components Global")]
    [SerializeField] private AudioSource globalAudioSource;

    [Header("Settings Components")]
    [SerializeField] private Animator SidePanelAnimator;
    public void OpenSettings()
    {
        Debug.Log("Settings Opened");
        // Add logic to open settings UI
        SidePanelAnimator.Play("SidePanel_in", 0, 0f);
    }

    public void CloseSettings()
    {
        Debug.Log("Settings Closed");
        SidePanelAnimator.Play("SidePanel_out", 0, 0f);
        // Add logic to close settings UI
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        StartCoroutine(LoadSceneWithTransition("ChooseLevel"));
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }

    public void ShowCredits()
    {
        Debug.Log("Credits Shown");
        StartCoroutine(LoadSceneWithTransition("Credit"));

    }

    public void PlaySfx(AudioClip audioClip)
    {
        globalAudioSource.Stop();
        globalAudioSource.clip = audioClip;
        globalAudioSource.Play();
    }
    public void PlaySfxWithRandPitch(AudioClip audioClip)
    {
        globalAudioSource.Stop();
        globalAudioSource.pitch = Random.Range(0.85f, 1.1f);
        globalAudioSource.clip = audioClip;
        globalAudioSource.Play();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToHome(string sceneName = "Home")
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

}