using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonLongPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    private float holdTime = 1.5f;

    [SerializeField]
    [Tooltip("Maximum duration to trigger the onClick event after pressing the button")]
    private float clickDuration = 0.3f;

    public UnityEvent onClick = new();
    public UnityEvent onLongPress = new();
    private bool isLongPressTriggered;

    private bool isPointerDown;
    private float pointerDownTimer;

    private void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= holdTime && !isLongPressTriggered)
            {
                isLongPressTriggered = true;
                onLongPress.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pointerDownTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
        pointerDownTimer = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isLongPressTriggered)
        {
            isPointerDown = false;
            isLongPressTriggered = false;
        }
        else
        {
            if (pointerDownTimer <= clickDuration) onClick.Invoke();

            isPointerDown = false;
        }
    }
}
