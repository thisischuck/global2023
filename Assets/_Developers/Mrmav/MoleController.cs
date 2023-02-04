using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoleController : MonoBehaviour
{

    public InputAction MovementInputAction;
    public InputAction MovementStickAction;
    public InputAction InteractInputAction;

    public GameObject RenderElement;

    public float Radius = 60.0f;

    public float ThetaIncrement = 1.0f;

    // the amout of follow direction when using the stick
    public float StickInputSize = 0.8f;

    [SerializeField]
    private float _currentTheta = 0.0f;

    private float _pi2 = Mathf.PI * 2;

    void OnEnable()
    {
        MovementInputAction.Enable();
        MovementStickAction.Enable();
        InteractInputAction.Enable();
    }

    void OnDisable()
    {
        MovementInputAction.Disable();
        MovementStickAction.Disable();
        InteractInputAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 stickValues = MovementStickAction.ReadValue<Vector2>();
        // Debug.Log("stick values " + stickValues);

        float interact = InteractInputAction.ReadValue<float>();
        // Debug.Log("Interact " + interact);

        if (stickValues.magnitude > 0f)
        {

            Vector3 side = Vector3.Lerp(transform.position.normalized, stickValues.normalized, 0.5f);
            
            Vector3 direction = side.normalized - transform.position.normalized;

            direction = direction * StickInputSize;
            Vector3 newPosition = transform.position.normalized + direction;

            transform.position = newPosition * Radius;

            float _angle = Vector3.Angle(transform.position, Vector3.right); 
            if (transform.position.y < 0)
                {
                    _angle = -_angle;
                }

            _currentTheta = _angle * Mathf.Deg2Rad;

            Debug.DrawLine(Vector3.zero, side * Radius, Color.blue);
            Debug.DrawLine(Vector3.zero, stickValues, Color.red);
            Debug.DrawLine(Vector3.zero, transform.position, Color.green);

        } else
        {

            Vector2 input = MovementInputAction.ReadValue<Vector2>();
            _currentTheta += ThetaIncrement * input.x * Time.deltaTime;

            transform.position = new Vector3(Mathf.Cos(_currentTheta) * Radius, Mathf.Sin(_currentTheta) * Radius, 0f);

        }     

        float angle = Vector3.Angle(transform.position, Vector3.right);        
        if (transform.position.y < 0)
        {
            angle = -angle;
        }
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }


    public float GetAngle()
    {
        float angle = Vector3.Angle(transform.position, Vector3.right);        
        if (transform.position.y < 0)
        {
            angle = -angle;
        }
        return angle;
    }

}
