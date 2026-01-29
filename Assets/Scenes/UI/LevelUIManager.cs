using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [Header("Components Global")]
    [SerializeField] private AudioSource globalAudioSource;

    [Header("Component UI")]
    [SerializeField] private Animator SideMenuAnimator;
    [SerializeField] private TMP_Text levelTextUI;
    [SerializeField] private TMP_Text dayTextUI;
    [SerializeField] private CanvasGroup plusIconCG, minusIconCG;

    private int level = 1;
    private int levelMax;
    private List<string> days = new List<string> { "Sunday ", "Monday ", "Tuesday ", "Wednesday ", "Thursday ", "Friday ", "Saturday " };

    private void Start()
    {
        // set default as false
        UpdateCanvasGroup(false, plusIconCG);
        UpdateCanvasGroup(false, minusIconCG);

        CheckLastLevel();
        UpdateLevelDayUI();

        if (level > 1)
        {
            UpdateCanvasGroup(true, plusIconCG);
            UpdateCanvasGroup(true, minusIconCG);
        }

        StartCoroutine(PrepareUI());
    }

    public void CheckLastLevel()
    {
        int LastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        level = LastLevel;
        levelMax = LastLevel;
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
        if (sceneName == "Home")
        {
            StartCoroutine(LoadSceneWithTransition(sceneName));
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator PrepareUI()
    {
        yield return new WaitForSeconds(2f);
        SideMenuAnimator.Play("SidePanel_in", 0, 0f);
    }


    public void AddLevelUI()
    {
        if (level == levelMax)
        {
            UpdateCanvasGroup(false, plusIconCG);
            return;
        }
        level += 1;
        UpdateLevelDayUI();
        UpdateCanvasGroup(true, plusIconCG);
    }

    public void DecreaseLevelUI()
    {
        if (level == 1)
        {
            UpdateCanvasGroup(false, minusIconCG);
            return;
        }

        level -= 1;
        UpdateLevelDayUI();
        UpdateCanvasGroup(true, minusIconCG);
    }

    void UpdateLevelDayUI()
    {
        levelTextUI.text = level.ToString();
        dayTextUI.text = days[(level - 1) % days.Count];
    }

    public void PlayLevel()
    {
        StartCoroutine(LoadSceneWithTransition("Level" + level.ToString()));
    }

    public void UpdateCanvasGroup(bool isActive, CanvasGroup canvasGroup)
    {
        if (isActive)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

}