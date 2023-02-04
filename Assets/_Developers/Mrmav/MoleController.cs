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

    // the amout of follow direction when using the stick
    public float StickInputSize = 3f;


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
        ApplyForce(new Vector3(0.0f, -1.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 stickValues = MovementStickAction.ReadValue<Vector2>();
        Vector3 force = Vector3.zero;

        if (stickValues.magnitude > 0f)
        {

            ApplyForce(CalculateMovementForce(stickValues));

        } else
        {
            Vector2 input = MovementInputAction.ReadValue<Vector2>();
            Vector3 fakeStick = Vector3.zero;

            // generate a fake stick input
            if (input.magnitude > 0f)
            {
                if (input.x > 0)
                    // emulate stick input that point 90deg to the right of the mole
                    fakeStick = new Vector3(-transform.position.normalized.y, transform.position.normalized.x);
                else
                    fakeStick = new Vector3(transform.position.normalized.y, -transform.position.normalized.x);
            }
            
            if(fakeStick.magnitude > 0.0f)
                ApplyForce(CalculateMovementForce(fakeStick.normalized));

        }     

        transform.rotation = Quaternion.Euler(0f, 0f, GetAngle());

    }

    public Vector3 CalculateMovementForce(Vector3 input)
    {
        
        // calculates the inbetween vector
        // this is used to get the mirror plane
        Vector3 side = Vector3.Lerp(transform.position.normalized, input.normalized, 0.5f);
        
        // the direction is a vector point towrds the desired destination
        Vector3 direction = side.normalized - transform.position.normalized;

        // scale up the direction and delta
        Vector3 force = direction * StickInputSize * Time.deltaTime;


        Debug.DrawLine(Vector3.zero, side * Radius, Color.blue);
        Debug.DrawLine(Vector3.zero, input * Radius, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + direction * 4.0f, Color.red);
        Debug.DrawLine(Vector3.zero, transform.position, Color.green);

        return force;


    }

    public void ApplyForce(Vector3 force)
    {
        transform.position = (transform.position.normalized + force) * Radius;
        
    }


    // Returns the angle at which the controller is.
    // this is a value between 0 to 180 or 0 to -180.
    //    -90 is up
    //     90 is down
    //      0 is right
    // (-)180 is left
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