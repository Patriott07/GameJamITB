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
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ChangeImage());
        }
    }
    public IEnumerator ChangeImage()
    {
        spriteRenderer.sprite = sprite;
        yield return new WaitForSeconds(0.5f);
        MouseController.instance.lose.SetActive(true);
        Time.timeScale = 0;
    }
}
