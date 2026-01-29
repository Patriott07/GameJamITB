using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level7ChangeScene : MonoBehaviour
{
    public string levelToLoad = "Level8";
    bool isCanLoadScene = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isCanLoadScene) return;
            isCanLoadScene = false;
            MouseController.instance.controll = false;
            StartCoroutine(LoadSceneWithTransition(levelToLoad));
        }
    }

   IEnumerator LoadSceneWithTransition(string sceneName)
    {
        SceneManager.LoadScene("Transition_in", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
