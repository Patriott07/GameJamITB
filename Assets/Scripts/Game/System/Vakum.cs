using UnityEngine;

public class Vakum : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public bool horizontal, vakumBaik;

    [Header("Controll Movement Settings")]
    public float minSpeed = 2f;
    public float maxSpeed = 20f;
    public float maxDistance = 6f;
    public float rotationSpeed = 720f;

    bool isControled;
    Camera mainCamera;
    Collider2D col;

    public Transform snapPoint;

    private Vector2 startPos;
    private Vector2 targetOffset;
    private bool goingForward = true;

    private void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        startPos = transform.position;

        if (horizontal)
            targetOffset = Vector2.right * moveDistance;
        else
            targetOffset = Vector2.up * moveDistance;
    }

    private void Update()
    {
        if (isControled && MouseController.instance.isJumping == false)
        {
            FollowMouseWithDynamicSpeed();
            moveDistance = 0;
            moveSpeed = 0;
            if (Input.GetMouseButtonDown(1))
            {
                col.isTrigger = true;
                MouseController.instance.TurunVakum();
                isControled = false;
            }
        }
        Vector2 targetPos = startPos + (goingForward ? targetOffset : -targetOffset);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            goingForward = !goingForward;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (MouseController.instance.isJumping && vakumBaik && other.gameObject.tag == "Player")
        {
            MouseController.instance.NaikVakum();
            Vector3 targetPos;

            if (snapPoint != null)
                targetPos = snapPoint.position;
            else
                targetPos = transform.position;

            other.transform.position = targetPos;
            other.transform.SetParent(transform);
            isControled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            col.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!vakumBaik && collision.gameObject.tag == "Player")
        {
            Debug.Log("Kalah");
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
}
