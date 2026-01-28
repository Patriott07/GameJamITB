using UnityEngine;

public class Vakum : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistanceHorizontal = 3f;
    public float moveDistanceVertical = 2f;
    public float moveSpeed = 2f;
    public bool horizontal, vertical;
    public bool vakumBaik;

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
    private Vector2 targetPos;

    private enum MoveState { Right, Up, Left, Down }
    private MoveState currentState = MoveState.Right;

    private void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();

        startPos = transform.position;
        if (vertical && !horizontal)
            currentState = MoveState.Up;
        else if (horizontal && !vertical)
            currentState = MoveState.Right;
        else
            currentState = MoveState.Right;
        SetNextTarget();
    }

    private void Update()
    {
        if (isControled && MouseController.instance.isJumping == false)
        {
            FollowMouseWithDynamicSpeed();
            RotateTowardMouse();

            moveSpeed = 0;

            if (Input.GetMouseButtonDown(1))
            {
                col.isTrigger = true;
                MouseController.instance.TurunVakum();
                isControled = false;
                currentState = MoveState.Right;
                SetNextTarget();
            }

            return;
        }

        // ===== ROAMING =====
        Vector2 beforePos = transform.position;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        Vector2 moveDir = targetPos - (Vector2)transform.position;

        RotateInstantByDirection(moveDir);

        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            startPos = transform.position;
            AdvanceState();
            SetNextTarget();
        }
    }


    void AdvanceState()
    {
        if (horizontal && vertical)
        {
            // kanan -> atas -> kiri -> bawah
            currentState = (MoveState)(((int)currentState + 1) % 4);
        }
        else if (horizontal)
        {
            currentState = (currentState == MoveState.Right) ? MoveState.Left : MoveState.Right;
        }
        else if (vertical)
        {
            currentState = (currentState == MoveState.Up) ? MoveState.Down : MoveState.Up;
        }
    }

    void SetNextTarget()
    {
        Vector2 dir = Vector2.zero;
        float dist = 0f;

        if (horizontal && vertical)
        {
            switch (currentState)
            {
                case MoveState.Right:
                    dir = Vector2.right;
                    dist = moveDistanceHorizontal;
                    break;

                case MoveState.Up:
                    dir = Vector2.up;
                    dist = moveDistanceVertical;
                    break;

                case MoveState.Left:
                    dir = Vector2.left;
                    dist = moveDistanceHorizontal;
                    break;

                case MoveState.Down:
                    dir = Vector2.down;
                    dist = moveDistanceVertical;
                    break;
            }
        }
        else if (horizontal)
        {
            dir = (currentState == MoveState.Left) ? Vector2.left : Vector2.right;
            dist = moveDistanceHorizontal;
        }
        else if (vertical)
        {
            dir = (currentState == MoveState.Down) ? Vector2.down : Vector2.up;
            dist = moveDistanceVertical;
        }

        targetPos = startPos + dir * dist;
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
            Time.timeScale = 0;
            MouseController.instance.lose.SetActive(true);
        }
    }

    private void FollowMouseWithDynamicSpeed()
    {
        Vector2 targetPos = GetWorldPosFromMouse();
        float distance = Vector2.Distance(transform.position, targetPos);

        float t = Mathf.Clamp01(distance / maxDistance);
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            currentSpeed * Time.deltaTime
        );
    }

    private Vector2 GetWorldPosFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void RotateTowardMouse()
    {
        Vector2 dir = GetWorldPosFromMouse() - (Vector2)transform.position;

        if (dir.sqrMagnitude < 0.001f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.Euler(0, 0, angle - 90f);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    void RotateInstantByDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // -90f kalau sprite default-nya menghadap ke atas
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }


}
