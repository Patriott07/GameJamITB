using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectSceneAfterTime : MonoBehaviour
{
    [Header("Redirection Settings")]
    [SerializeField] private string sceneName;
    [SerializeField] private float delayTime = 5f;

    void Start()
    {
        StartCoroutine(RedirectToNewScene());   
    }

    IEnumerator PrepareTransition()
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(3f);

    }

    IEnumerator RedirectToNewScene()
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(PrepareTransition());
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
