using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = true;

    private VirtualJoystick vJoystick;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        vJoystick = GameObject.Find("BackGroundImage").GetComponent<VirtualJoystick>();
    }

    private void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 
            1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = vJoystick.Horizontal();
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector2 currentVelocity = rb2d.velocity;
        
        if (horizontal * currentVelocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * horizontal * moveForce);
        }

        if (Mathf.Abs(currentVelocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(currentVelocity.x) * maxSpeed, currentVelocity.y);

        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
