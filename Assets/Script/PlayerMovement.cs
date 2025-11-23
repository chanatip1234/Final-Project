using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f; 

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        
        if (moveInput != 0)
        {
            if (anim != null) anim.SetBool("isRunning", true);
            
            if (moveInput > 0) transform.localScale = new Vector3(-1, 1, 1);       
            else if (moveInput < 0) transform.localScale = new Vector3(1, 1, 1); 
        }
        else
        {
            if (anim != null) anim.SetBool("isRunning", false);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }
}
