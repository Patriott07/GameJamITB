using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadSceneOnStart : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneName = "Transition_out";
    [SerializeField] private bool afterShowFading = false;
    [SerializeField] private float delayTime = 1f;

    void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        if(afterShowFading)
        {
            StartCoroutine(UnloadSceneAfterDelay(delayTime));
        }
    }
    IEnumerator UnloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
