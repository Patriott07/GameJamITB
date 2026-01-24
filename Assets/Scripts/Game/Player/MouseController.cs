using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float maxDistance = 6f;

    bool controll;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (controll)
        {
            FollowMouseWithDynamicSpeed();
        }
    }

    private void FollowMouseWithDynamicSpeed()
    {
        Vector2 targetPos = GetWorldPosFromMouse();
        float distance = Vector2.Distance(transform.position, targetPos);

        float t = Mathf.Clamp01(distance / maxDistance);

        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);


        transform.position = Vector2.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);
    }

    private Vector2 GetWorldPosFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        controll = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kaca")
        {
            Debug.Log("Kalah");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Menang");
        }
    }
}
