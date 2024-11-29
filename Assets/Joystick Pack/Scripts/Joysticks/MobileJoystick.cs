using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    public float joystickRadius = 50f;

    public Vector2 joystickCenter;
    private int joystickTouchId = -1;

    void Start()
    {
        joystickCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (joystickTouchId == -1 && Vector2.Distance(touch.position, joystickCenter) < joystickRadius)
                {
                    joystickTouchId = touch.fingerId;
                }
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.fingerId == joystickTouchId)
                {
                    Vector2 touchDelta = touch.position - joystickCenter;
                    Vector2 normalizedInput = touchDelta.normalized;
                    float clampedMagnitude = Mathf.Clamp(touchDelta.magnitude, 0f, joystickRadius);
                    Vector2 joystickInput = normalizedInput * clampedMagnitude / joystickRadius;

                    // Use joystickInput for your mobile movement logic
                    Debug.Log(joystickInput);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (touch.fingerId == joystickTouchId)
                {
                    joystickTouchId = -1;
                }
            }
        }
    }
}
