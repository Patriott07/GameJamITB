using UnityEngine;

public class CameraEdge : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    public float moveSpeed = 5f;           // Kecepatan gerak kamera
    public float edgeSize = 50f;           // Jarak dari tepi layar yang memicu gerakan
    public bool clampMovement = true;      // Batasi area gerak kamera?
    public Vector2 xLimit = new Vector2(-10f, 10f); // Batas gerak X
    public Vector2 yLimit = new Vector2(-5f, 5f);   // Batas gerak Y (kalau 2D)

   
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Posisi dasar kamera (mengikuti target + offset)
        Vector3 desiredPos = transform.position;

        // Tambahkan pergeseran berdasarkan posisi mouse di tepi layar
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= Screen.width - edgeSize)
            desiredPos.x += 1f; // geser ke kanan
        else if (mousePos.x <= edgeSize)
            desiredPos.x -= 1f; // geser ke kiri

        if (mousePos.y >= Screen.height - edgeSize)
            desiredPos.y += 1f; // geser ke atas
        else if (mousePos.y <= edgeSize)
            desiredPos.y -= 1f; // geser ke bawah

        // Gerakan halus
        transform.position = Vector3.Lerp(transform.position, desiredPos, moveSpeed * Time.deltaTime);

        // Batasi area (opsional)
        if (clampMovement)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, xLimit.x, xLimit.y),
                Mathf.Clamp(transform.position.y, yLimit.x, yLimit.y),
                transform.position.z
            );
        }
    }
}
