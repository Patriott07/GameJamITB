using UnityEngine;
public class PushableObject : MonoBehaviour
{
    [SerializeField] private float pushResistance = 1f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
