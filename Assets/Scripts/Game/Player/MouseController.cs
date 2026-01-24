using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private float maxSpeed = 10;
    bool controll;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //if (controll == true)
        //{
        //}
            delayFollowing(maxSpeed);
    }

    private void delayFollowing(float maxSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPosFromMouse(), maxSpeed * Time.deltaTime);
    }

    private Vector2 GetWorldPosFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    //private void OnMouseDown()
    //{
    //    if (controll == false)
    //    {
    //        controll = true;
    //    }
    //    //else
    //    //{
    //    //    controll = false;
    //    //}
    //}
}
