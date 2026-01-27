using System.Collections;
using UnityEngine;
public class PushableObject : MonoBehaviour
{
    [SerializeField] private float pushResistance = 1f;

    Rigidbody2D rb;
    Collider2D col;
    public Transform snapPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (MouseController.instance.abovePushable && Input.GetMouseButtonDown(1))
        {
            col.isTrigger = true;
            StartCoroutine(Delay(1));
            col.isTrigger = false;
        }
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force / pushResistance, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kaca")
        {
            Debug.Log("kalah");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(MouseController.instance.isJumping && collision.gameObject.CompareTag("Player"))
        { 
            Vector3 targetPos;

            if (snapPoint != null)
                targetPos = snapPoint.position;
            else
                targetPos = transform.position;
            MouseController.instance.abovePushable = true;
            collision.transform.position = targetPos;
            collision.transform.SetParent(transform);
            MouseController.instance.NaikBox();
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        col.isTrigger = false;
    //    }
    //}
}