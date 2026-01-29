using System.Collections;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // state kalah
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable") || collision.CompareTag("Vakum"))
        {
            if(MouseController.instance.kalah) return;
            
            MouseController.instance.kalah = true;
            Debug.Log("Kamu kalah");
            StartCoroutine(ChangeImage());
            AudioManager.Instance.PlayObjectAudio(AudioManager.Instance.pecahObj);
            AudioManager.Instance.CatScream();
        }
    }
    public IEnumerator ChangeImage()
    {
        spriteRenderer.sprite = sprite;
        MouseController.instance.controll = false;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
        ChangeCursor.instance.SetDefaultCursor();
        MouseController.instance.SetCanvasGroup(MouseController.instance.loseCanvasGroup, true);
        AudioManager.Instance.PlayLoseAudio();
    }
}
