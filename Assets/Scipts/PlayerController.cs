using UnityEngine;

public class PlayerController : MonoBehaviour
{
   // [Header("Movement parameters")];
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [Range(0.01f, 20.0f)] [SerializeField] private float jumpForce = 6.0f;
    //[Space(10)];
    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool IsRunning = false;
    private bool IsFacingRight = true;
    [SerializeField] private LayerMask groundLayer;
    const float rayLength = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        IsRunning = false;
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            IsRunning = true;
            if (!IsFacingRight)
            {
                Flip();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
           transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
           IsRunning = true;
           if (IsFacingRight)
            {
                Flip();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            IsRunning = true;
            if (!IsFacingRight)
            {
                Flip();
            }
        }
        if ( Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            IsRunning = true;
            if (IsFacingRight)
            {
                Flip();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("IsRunning", IsRunning);
        Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 0.2f, false);
    }
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", false);
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("jumping");
        }
    }
   void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LevelExit"))
        {
            Debug.Log("Game Over - Exit");
        }
        else if (col.CompareTag("LevelFall"))
        {
            Debug.Log("Game Over - Fall");
        }
        else if (col.CompareTag("Bonus"))
        {
            Debug.Log("Bonus achieved");
            col.gameObject.SetActive(false);
        }
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
