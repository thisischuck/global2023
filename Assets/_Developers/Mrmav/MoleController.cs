using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MoleController : MonoBehaviour
{
    public InputAction MovementInputAction;
    public InputAction MovementStickAction;
    public InputAction InteractInputAction;

    public GameObject RenderElement;

    private float _radius = 3f;

    private float _slideTime = 0f;

    private Vector2 _input = Vector2.zero;

#region Delegates
    public delegate void OnEatDelegate();
    public delegate void OnSlideDelegate();
    public delegate void OnInteractDelegate(float currentAngle);
    public delegate void OnUnlockItemDelegate(Item item);

    public OnEatDelegate OnEat;
    public OnSlideDelegate OnSlide;
    public OnInteractDelegate OnInteract;
    public OnUnlockItemDelegate OnUnlockItem;

#endregion
#region Stats
    [Header("Stats")]
    [SerializeField] private List<Item> _unlockedItems;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _slideForce = 2.0f;
    [SerializeField] float _biteDamage;
    [SerializeField] float _biteCooldown;
    [SerializeField] float _vignetteAmount;
#endregion

    [Header("Cap Amounts")]
    [SerializeField] [MinMaxRange(.1f, 10)]RangedFloat _minMaxDamage;
    [SerializeField] [MinMaxRange(.5f, 2)] RangedFloat _minMaxScale;

    private float _slideDelay = 0.25f;

    private bool _isOnCooldown = false;

    private SpriteRenderer _sprite = null;
    private Volume _pp = null;
    private Animator _animator;

    private bool _wasSliding = false;

    private float speed;
    private Vector3 oldPosition;

    private int _touchInput = 0;
    public int TouchInput { get => _touchInput; set => _touchInput = value; }

    void OnEnable()
    {
        MovementInputAction.Enable();
        MovementStickAction.Enable();
        InteractInputAction.Enable();

        InteractInputAction.performed += ctx => OnInteract?.Invoke(MathTools.To360(GetAngle()));
    }

    void OnDisable()
    {
        MovementInputAction.Disable();
        MovementStickAction.Disable();
        InteractInputAction.Disable();
        
        InteractInputAction.performed -= ctx => OnInteract?.Invoke(MathTools.To360(GetAngle()));
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _sprite = RenderElement.GetComponent<SpriteRenderer>();

        ApplyForce(new Vector3(0.0f, -1.0f, 0.0f));
        oldPosition = this.transform.position;
    }

    public void UnlockItem(Item itemToUnlock)
    {
        _unlockedItems.Add(itemToUnlock);
        OnUnlockItem?.Invoke(itemToUnlock);
        
        UpdateStats();
    }

    void UpdateStats()
    {
        foreach (var item in _unlockedItems)
        {
            if(item is Upgrade)
            {
                var up = (Upgrade)item;
                _speed += up.SpeedIncrement;
                _biteDamage += up.DamageIncrement;
                _slideForce -= up.SlideDecrement;
            }
        }

        UpdateVFX();
    }

    void UpdateVFX()
    {
        var newScale = new Vector3( MathTools.Remap(_biteDamage,_minMaxDamage.minValue,_minMaxDamage.maxValue,_minMaxScale.minValue, _minMaxScale.maxValue), MathTools.Remap(_biteDamage,_minMaxDamage.minValue,_minMaxDamage.maxValue,_minMaxScale.minValue, _minMaxScale.maxValue),transform.localScale.z);
        _sprite.transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickValues = MovementStickAction.ReadValue<Vector2>();
        Vector2 kbInput = MovementInputAction.ReadValue<Vector2>();
        Vector3 force = Vector3.zero;
        _input = stickValues;



        bool thereWasInput = false;

        if (stickValues.magnitude > 0f)
        {
            // joystick
            force = CalculateMovementForce(stickValues);
            thereWasInput = true;
            _input = stickValues;

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

            _input = fakeStick;

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

            _input = fakeStick;

            force = CalculateMovementForce(fakeStick);

            thereWasInput = true;
        }


        _animator.SetBool("Walking", force.magnitude > 0);
        ApplyForce(force);

        if (!thereWasInput)
        {
            _slideTime += Time.deltaTime;
            Debug.DrawLine(transform.position, transform.position + Vector3.down * _slideForce, Color.magenta);
        }
        else
        {
            _slideTime = 0;

        }

        if (IsSliding())
        {
            _animator.SetBool("Sliding", true);
            ApplyForce(Vector3.down * _slideForce * Time.deltaTime);
        } else
        {
            _wasSliding = false;
            _animator.SetBool("Sliding", false);
        }


        transform.rotation = Quaternion.Euler(0f, 0f, GetAngle() + 90);
        //RenderElement.transform.rotation = Quaternion.Euler(0f, 0f, GetAngle() + 90);

        // determine which side the force was applied at
        // and flip de sprite if needed
        Vector3 right = Vector3.Cross(transform.position.normalized, Vector3.forward);
        float dot = Vector3.Dot(force.normalized, right.normalized);

        Flip(dot);
        //Debug.Log("Angle: " + GetAngle() + "___ To360: " + (360f - MathTools.To360(GetAngle())));
        oldPosition = this.transform.position;
    }

    private void Flip(float dot)
    {
        // if (dot > 0)
        //     _sprite.flipX = true;
        // else if (dot < 0)
        //     _sprite.flipX = faslse;

        if (dot > 0)
            transform.localScale = new Vector3(-1, 1, 0);
        else if (dot < 0)
            transform.localScale = new Vector3(1, 1, 0);
    }

    public Vector3 CalculateMovementForce(Vector3 input)
    {
        // calculates the inbetween vector
        // this is used to get the mirror plane
        Vector3 side = Vector3.Lerp(transform.position.normalized, input.normalized, 0.5f);

        // the direction is a vector point towrds the desired destination
        Vector3 direction = side.normalized - transform.position.normalized;

        // scale up the direction and delta
        Vector3 force = direction * _speed * Time.deltaTime;


        Debug.DrawLine(Vector3.zero, side * _radius, Color.blue);
        Debug.DrawLine(Vector3.zero, input * _radius, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + direction * 4.0f, Color.red);
        Debug.DrawLine(Vector3.zero, transform.position, Color.green);

        return force;
    }

    public void ApplyForce(Vector3 force)
    {
        transform.position = (transform.position + force).normalized * _radius;
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

    public Vector2 GetInput()
    {
        return _input;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Root") return;
        if (_isOnCooldown) return;

        StartCoroutine(COR_Bite(other.GetComponentInParent<RootAnimation>()));
    }

    private IEnumerator COR_Bite(RootAnimation biteTarget)
    {
        _isOnCooldown = true;
        biteTarget.LoseHealth(_biteDamage );
        Debug.Log("Damaged: " + _biteDamage);
        OnEat?.Invoke();
        yield return Yielders.Get(_biteCooldown);
        _isOnCooldown = false;
    }


    public bool IsSliding()
    {   
        bool slide = _slideTime > _slideDelay;

        if (slide && !_wasSliding)
        {
            OnSlide?.Invoke();
            _wasSliding = true;
        }

        return slide;
    }

}
