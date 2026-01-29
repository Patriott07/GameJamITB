using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class Text3DInteraction : MonoBehaviour, 
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("3D Text Style Hover")]
    [SerializeField] private Color hoverColor = Color.yellow;

    [Header("Events")]
    [SerializeField] private UnityEvent onClick, onExit, onHover;

    private Color originalColor;
    private TMP_Text textMeshPro;

    public void Start()
    {
        textMeshPro = gameObject.GetComponent<TMP_Text>();
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover");
        if (textMeshPro != null)
        {
            textMeshPro.color = hoverColor;
        }
        onHover?.Invoke();
    }
    

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        if (textMeshPro != null)
        {
            textMeshPro.color = originalColor;
        }
        onExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        onClick?.Invoke();
    }
}
