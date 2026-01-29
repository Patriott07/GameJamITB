using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
public class HUDUIManager : MonoBehaviour
{
    [Header("Components Global")]
    [SerializeField] private AudioSource globalAudioSource;

    [Header("Settings Components")]
    [SerializeField] private Animator SidePanelAnimator;



    bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            if (isPaused)
                ClosePausedMenu();
            else
                OpenPausedMenu();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        MouseController.instance.SetCanvasGroup(MouseController.instance.loseCanvasGroup, false);
        MouseController.instance.SetCanvasGroup(MouseController.instance.winCanvasGroup, false);

        StartCoroutine(LoadSceneWithTransition(SceneManager.GetActiveScene().name));
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("LevelPlay", 1);
        PlayerPrefs.SetInt("LevelPlay", PlayerPrefs.GetInt("LevelPlay") + 1);
        StartCoroutine(LoadSceneWithTransition("Level" + (currentLevel + 1)));
    }

    public void OpenPausedMenu()
    {
        ChangeCursor.instance.SetDefaultCursor();

        Debug.Log("Pause panel Opened");
        SidePanelAnimator.Play("SidePanel_in", 0, 0f);
        isPaused = !isPaused;
        MouseController.instance.controll = false;
    }

    public void ClosePausedMenu()
    {
        ChangeCursor.instance.SetGameCursor();

        Debug.Log("Pause panel Closed");
        SidePanelAnimator.Play("SidePanel_out", 0, 0f);
        isPaused = !isPaused;

        MouseController.instance.controll = true;
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
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCreditScene()
    {
        StartCoroutine(LoadSceneWithTransition("Credit"));
    }
}
