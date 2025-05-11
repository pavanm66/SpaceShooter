/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public Image joyStickBg;
    public Image joyStickKnob;
    public float maxSpeed = 5f;  // Maximum movement speed
    [SerializeField] private RectTransform knobRectTransform;
    [SerializeField] private RectTransform bgRectTransform;
    private Vector2 joyStickCenter;
    
    Camera sceneCamera;
    // Start is called before the first frame update
    private Vector2 joystickPosition;

    private Vector2 inputVector;
    public float speed = 5f;
    public float acceleration = 2f;
   [SerializeField] Rigidbody2D rb;

    void Start()
    {
        knobRectTransform = joyStickKnob.GetComponent<RectTransform>();
        bgRectTransform = joyStickBg.GetComponent<RectTransform>();
     //   rb = player.GetComponent<Rigidbody2D>();
        // Get the center of the joystick background in local space
        joyStickCenter = bgRectTransform.localPosition;
        joystickPosition = bgRectTransform.anchoredPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position);

        position = position / (bgRectTransform.sizeDelta / 2);

        // Clamp joystick input to keep within the round boundary
        if (position.magnitude > 1)
        {
            position = position.normalized;
        }

        inputVector = position;

        // Move the joystick handle
        knobRectTransform.anchoredPosition = new Vector2(
            inputVector.x * (bgRectTransform.sizeDelta.x / 2),
            inputVector.y * (bgRectTransform.sizeDelta.y / 2));
    }

    private void Update()
    {
        Vector3 movementDirection = new Vector2(inputVector.x, inputVector.y);

        if (movementDirection.magnitude > 0.1f)
        {
            // Apply continuous force in the movement direction
            rb.AddForce(movementDirection * speed * acceleration * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Limit the maximum speed to avoid the object going too fast
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       OnDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        knobRectTransform.anchoredPosition = Vector2.zero;
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image joyStickBg;
    public Image joyStickKnob;
    public float maxSpeed = 5f;  // Maximum movement speed
    [SerializeField] private RectTransform knobRectTransform;
    [SerializeField] private RectTransform bgRectTransform;
    private Vector2 joyStickCenter;

    private Vector2 inputVector;
    public float speed = 5f;
    public float acceleration = 2f;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        knobRectTransform = joyStickKnob.GetComponent<RectTransform>();
        bgRectTransform = joyStickBg.GetComponent<RectTransform>();
        joyStickCenter = bgRectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position);

        position = position / (bgRectTransform.sizeDelta / 2);

        // Clamp joystick input to keep within the round boundary
        if (position.magnitude > 1)
        {
            position = position.normalized;
        }
        isDragging = true;
        inputVector = position;

        // Move the joystick handle
        knobRectTransform.anchoredPosition = new Vector2(
            inputVector.x * (bgRectTransform.sizeDelta.x / 2),
            inputVector.y * (bgRectTransform.sizeDelta.y / 2));

        print(isDragging + " is dragging in ondrag pavan");

    }
    [SerializeField] bool isDragging;
    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick handle position and input vector when releasing
        inputVector = Vector2.zero;
        knobRectTransform.anchoredPosition = Vector2.zero;
        isDragging = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Do nothing here, as OnDrag will handle movement
    }

    private void Update()
    {
        Vector2 movementDirection = new Vector2(inputVector.x, inputVector.y);
        if (isDragging)
        {
            if (movementDirection.magnitude > 0.1f)
            {
                rb.AddForce(acceleration * speed * Time.deltaTime * movementDirection, ForceMode2D.Impulse);
            }
        }
        else
        {
            print(isDragging + " is dragging pavan");
            rb.AddForce(Vector2.zero);
        }
#if UNITY_EDITOR
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical);

        rb.AddForce(movementDirection * acceleration * speed * Time.deltaTime, ForceMode2D.Impulse);
#endif
    }

    private void FixedUpdate()
    {
        // Limit the maximum speed to avoid the object going too fast
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        float clampedX = Mathf.Clamp(rb.transform.localPosition.x, -7.6f, 7.6f);
        float clampedY = Mathf.Clamp(rb.transform.localPosition.y, -3.98f, 3.98f);

        // Apply the clamped position
        rb.transform.localPosition = new Vector2(clampedX, clampedY);
    }
}
