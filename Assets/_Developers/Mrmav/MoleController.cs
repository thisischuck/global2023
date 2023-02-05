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

    // the slide effect of the mole
    public float SlideForce = 2.0f;


    private SpriteRenderer _sprite = null;
    private Vector3 _slideVelocity = Vector3.zero;
    private Animator _animator;

    private float speed;
    private Vector3 oldPosition;

    private int _touchInput = 0;
    public int TouchInput { get => _touchInput; set => _touchInput = value; }

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
        _animator = this.GetComponent<Animator>();
        _sprite = RenderElement.GetComponent<SpriteRenderer>();

        ApplyForce(new Vector3(0.0f, -1.0f, 0.0f));
        oldPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickValues = MovementStickAction.ReadValue<Vector2>();
        Vector2 kbInput = MovementInputAction.ReadValue<Vector2>();
        Vector3 force = Vector3.zero;

        bool thereWasInput = false;

        if (stickValues.magnitude > 0f)
        {
            // joystick
            force = CalculateMovementForce(stickValues);
            thereWasInput = true;

        }
        else if (kbInput.magnitude > 0.01f)
        {
            // kb input
            // generate a fake stick input
            Vector3 fakeStick = Vector3.zero;

            if (kbInput.x > 0)
                // emulate stick input that points 90deg to the right of the mole
                fakeStick = new Vector3(-transform.position.normalized.y, transform.position.normalized.x);
            else
                fakeStick = new Vector3(transform.position.normalized.y, -transform.position.normalized.x);

            force = CalculateMovementForce(fakeStick);

            thereWasInput = true;

        }
        else if (_touchInput != 0)
        {
            // kb input
            // generate a fake stick input
            Vector3 fakeStick = Vector3.zero;

            if (_touchInput > 0)
                // emulate stick input that points 90deg to the right of the mole
                fakeStick = new Vector3(-transform.position.normalized.y, transform.position.normalized.x);
            else
                fakeStick = new Vector3(transform.position.normalized.y, -transform.position.normalized.x);

            force = CalculateMovementForce(fakeStick);

            thereWasInput = true;
        }


        _animator.SetBool("Walking", force.magnitude > 0);
        ApplyForce(force);

        if (!thereWasInput)
        {
            ApplyForce(Vector3.down * SlideForce * Time.deltaTime);
            if (CalculateSpeed() > 0.01f)
            {
                _animator.SetBool("Sliding", true);
            }
            else
            {
                _animator.SetBool("Sliding", false);
            }
            Debug.DrawLine(transform.position, transform.position + Vector3.down * SlideForce, Color.magenta);
        }
        else _animator.SetBool("Sliding", false);



        RenderElement.transform.rotation = Quaternion.Euler(0f, 0f, GetAngle() + 90);

        // determine which side the force was applied at
        // and flip de sprite if needed
        Vector3 right = Vector3.Cross(transform.position.normalized, Vector3.forward);
        float dot = Vector3.Dot(force.normalized, right.normalized);
        if (dot > 0)
            _sprite.flipX = true;
        else if (dot < 0)
            _sprite.flipX = false;
        // if 0, just leave it
        //Debug.Log("Angle: " + GetAngle() + "___ To360: " + (360f - MathTools.To360(GetAngle())));
        oldPosition = this.transform.position;
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

    public float CalculateSpeed()
    {
        float mag = (oldPosition - this.transform.position).magnitude;
        return mag;
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
