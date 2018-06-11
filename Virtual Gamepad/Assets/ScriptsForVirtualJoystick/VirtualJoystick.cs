using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImage;
    private Image joystickImage;
    private Vector2 inputVector;

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>(); //gets image of the child
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            // helps limit values to the extent of the circle
            inputVector = (inputVector.magnitude > 1) ? inputVector.normalized : inputVector;

            // moving the joystick image
            joystickImage.rectTransform.anchoredPosition = new Vector2(
                inputVector.x * (bgImage.rectTransform.sizeDelta.x/2.5f), 
                inputVector.y * (bgImage.rectTransform.sizeDelta.y/2.5f));
        }
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }

	public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
}
