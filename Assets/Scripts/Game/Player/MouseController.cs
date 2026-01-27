using System.Collections;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 2f;
    public float maxSpeed = 20f;
    public float maxDistance = 6f;

    [Header("Jump")]
    public float jumpScaleMultiplier = 1.3f;
    public float jumpDuration = 1f;
    public float jumpCooldown = 0.3f;

    [Header("Head Anchor")]
    public float headOffset = 0.6f;


    [Header("Push")]
    [SerializeField] private float pushForce = 8f;

    [Header("Rotation")]
    public float rotationSpeed = 720f;

    [Header("Win Lose Condition")]
    public GameObject win;
    public GameObject lose;

    Camera mainCamera;
    Rigidbody2D rb;
    [HideInInspector]
    public bool isJumping, controll, abovePushable, kalah;
    bool naikVakum;
    BoxCollider2D boxCollider;
    Vector3 originalScale;

    public static MouseController instance;


    private void Start()
    {
        instance = this;
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (controll)
        {
            FollowMouseWithDynamicSpeed();

            if (Input.GetMouseButtonDown(1) && !isJumping && !abovePushable)
            {
                StartCoroutine(Jumping());
            }
            else if (Input.GetMouseButtonDown(1) && !isJumping && abovePushable)
            {
                StartCoroutine(JumpingAbovePushable());
            }
        }

        if (abovePushable)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    private void FollowMouseWithDynamicSpeed()
    {
        Vector2 mouseWorldPos = GetWorldPosFromMouse();

        Vector2 toMouseDir = (mouseWorldPos - (Vector2)transform.position).normalized;

        Vector2 anchoredTargetPos = mouseWorldPos - toMouseDir * headOffset;

        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance(currentPos, anchoredTargetPos);

        float t = Mathf.Clamp01(distance / maxDistance);
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);

        Vector2 newPos = Vector2.MoveTowards(currentPos, anchoredTargetPos, currentSpeed * Time.deltaTime);

        Vector2 aimDir = mouseWorldPos - (Vector2)transform.position;

        if (aimDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        transform.position = newPos;
    }



    private Vector2 GetWorldPosFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if(!naikVakum)
        {
            controll = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Kalah");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            if (!kalah)
            {
                Debug.Log("Menang");
                win.SetActive(true);
                controll = false;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            PushableObject pushable = collision.gameObject.GetComponent<PushableObject>();
            if (pushable != null)
            {
                Vector2 dir = (collision.transform.position - transform.position).normalized;
                pushable.Push(dir * pushForce);
            }
        }
    }

    private IEnumerator Jumping()
    {
        isJumping = true;
        float halfDuration = jumpDuration / 2;
        Vector3 targetScale = originalScale * jumpScaleMultiplier;

        float time = 0;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float lerp = time / halfDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, lerp);
            yield return null;
        }

        time = 0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float lerp = time / halfDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, lerp);
            yield return null;
        }

        transform.localScale = originalScale;
        if (naikVakum)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        yield return new WaitForSeconds(jumpCooldown);
        isJumping = false;
    }

    private IEnumerator JumpingAbovePushable()
    {
        abovePushable = false;
        isJumping = true;
        boxCollider.enabled = false;
        float halfDuration = jumpDuration / 2;
        Vector3 targetScale = originalScale * jumpScaleMultiplier;

        float time = 0;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float lerp = time / halfDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, lerp);
            yield return null;
        }

        time = 0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            float lerp = time / halfDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, lerp);
            yield return null;
        }

        transform.localScale = originalScale;
        boxCollider.enabled = true;
        transform.SetParent(null);
        yield return new WaitForSeconds(jumpCooldown);
        isJumping = false;
    }

    public void NaikVakum()
    {
        naikVakum = true;
        boxCollider.enabled = false;
        controll = false;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TurunVakum()
    {
        transform.SetParent(null);
        controll = true;
        naikVakum = false;
        transform.localScale = originalScale;
        boxCollider.enabled = true;
    }

    public void NaikBox()
    {
        abovePushable = true;
        boxCollider.enabled = false;
        transform.localScale = Vector3.one;
    }
}
