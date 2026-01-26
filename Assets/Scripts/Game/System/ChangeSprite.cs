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
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable"))
        {
            StartCoroutine(ChangeImage());
        }
    }
    public IEnumerator ChangeImage()
    {
        spriteRenderer.sprite = sprite;
        MouseController.instance.controll = false;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
        MouseController.instance.lose.SetActive(true);
    }
}
