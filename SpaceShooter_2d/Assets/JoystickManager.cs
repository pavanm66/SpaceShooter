using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject joyStickBg;
    public GameObject joyStickKnob;
    public float maxSpeed = 5f;  // Maximum movement speed
    [SerializeField] private RectTransform knobRectTransform;
    [SerializeField] private RectTransform bgRectTransform;
    private Vector2 joyStickCenter;
    [SerializeField] Player player;
    Camera sceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        knobRectTransform = joyStickKnob.GetComponent<RectTransform>();
        bgRectTransform = joyStickBg.GetComponent<RectTransform>();

        // Get the center of the joystick background in local space
        joyStickCenter = bgRectTransform.localPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        print(eventData.pointerDrag + "  is pointer drag");

        // Get the drag position relative to the joystick background in local space
        Vector2 dragPosition;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(bgRectTransform, eventData.position, eventData.pressEventCamera, out dragPosition);
      //  dragPosition = RectTransformUtility.WorldToScreenPoint(sceneCamera, eventData.position);

        // Calculate the offset from the joystick center
        Vector2 offset = dragPosition - joyStickCenter;
        float dragDistance = offset.magnitude;

        // Clamp the drag distance within the bounds of the joystick background (radius)
        float radius = bgRectTransform.sizeDelta.x / 2;
        Vector2 direction = offset.normalized;  // Direction of drag

        // Move the knob within the joystick background radius
        if (dragDistance > radius)
        {
            dragPosition = joyStickCenter + direction * radius;  // Limit the knob to the edge of the joystick
        }


        // Update the joystick knob position relative to the joystick background
        knobRectTransform.anchoredPosition = dragPosition;

        // Calculate movement speed based on drag distance (relative to the joystick radius)
        float speedFactor = Mathf.Clamp01(dragDistance / radius);
        float currentSpeed = speedFactor * maxSpeed;

        // Move the player based on the direction of the joystick
        Vector3 movement = new Vector3(direction.x * currentSpeed, direction.y * currentSpeed, 0f) * Time.deltaTime * speedFactor;
        player.transform.position += movement;

        // Apply clamping for vertical movement within bounds (-4 to 4)
        if (player.transform.position.y > 4f)
        {
            player.transform.position = new Vector3(player.transform.position.x, 4f, player.transform.position.z);
        }
        else if (player.transform.position.y < -4f)
        {
            player.transform.position = new Vector3(player.transform.position.x, -4f, player.transform.position.z);
        }

        // Apply clamping for horizontal movement within bounds (-7.83 to -4.5)
        if (player.transform.position.x < -7.83f)
        {
            player.transform.position = new Vector3(-7.83f, player.transform.position.y, player.transform.position.z);
        }
        else if (player.transform.position.x > 7.4f)
        {
            player.transform.position = new Vector3(7.4f, player.transform.position.y, player.transform.position.z);
        }
    }

    // Called when the drag starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        //joyStickKnob.GetComponent<Image>().color = Color.red;
        //joyStickKnob.transform.position = eventData.position;
    }

    // Called when the drag ends
    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the joystick knob to the center after drag ends
        knobRectTransform.anchoredPosition = Vector2.zero;
        //joyStickKnob.GetComponent<Image>().color = Color.white;
    }


}
