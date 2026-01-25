using UnityEngine;

public class Vakum : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public bool horizontal = false;

    private Vector2 startPos;
    private Vector2 targetOffset;
    private bool goingForward = true;

    private void Start()
    {
        startPos = transform.position;

        if (horizontal)
            targetOffset = Vector2.right * moveDistance;
        else
            targetOffset = Vector2.up * moveDistance;
    }

    private void Update()
    {
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
}
