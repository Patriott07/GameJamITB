using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D gameCursor;
    public Vector2 hotspot = Vector2.zero;

    public static ChangeCursor instance;

    private void Awake()
    {
        if (instance != null )
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SetDefaultCursor();
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameCursor()
    {
        Cursor.SetCursor(gameCursor, hotspot, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
