using UnityEngine;
using UnityEngine.EventSystems;

public class Joystic : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]private RectTransform joystickBackground;
    [SerializeField]private RectTransform joystickHandle;
    public Vector2 joystickPosition;

    [Header("Options")]
    public float joystickRadius = 50f;

    void Start()
    {
       /* joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)joystickBackground.position;
        direction = Vector2.ClampMagnitude(direction, joystickRadius);

        joystickHandle.anchoredPosition = direction;

        joystickPosition = direction.normalized; // You can use this position for player movement
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero;
        joystickPosition = Vector2.zero; // Reset position on release
    }

    // Additional methods or events can be added based on your requirements
}
