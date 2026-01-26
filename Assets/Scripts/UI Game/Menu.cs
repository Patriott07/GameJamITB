using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panel;
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0)
        {
            if (panel != null)
            {
                panel.SetActive(true);
                Time.timeScale = 0;
                MouseController.instance.controll = false;
            }
        }
    }

    void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
