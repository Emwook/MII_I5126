using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [Range(0.01f, 20.0f)] [SerializeField] private float moveRange = 1.0f;
    private Animator animator;
    private float startPositionX;
    private bool IsmovingRight = false;
    private bool IsFacingRight = false;
    void Start()
    {
        
    }
     void Awake()
    {
        animator = GetComponent<Animator>();
        startPositionX = this.transform.position.x;
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void moveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if (!IsFacingRight)
        {
            Flip();
        }
    }
    void moveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if (IsFacingRight)
        {
            Flip();
        }
    }
    // Update is called once per frame
    void Update()
    {
    if (IsmovingRight)
    {
        moveRight();

        if (transform.position.x >= startPositionX + moveRange)
        {
            IsmovingRight = false;
        }
    }
    else
    {
        moveLeft();

        if (transform.position.x <= startPositionX - moveRange)
        {
            IsmovingRight = true;
        }
    }
}

}
