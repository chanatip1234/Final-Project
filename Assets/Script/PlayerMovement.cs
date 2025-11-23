using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private Animator anim;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);


        if (moveInput != 0) 
        {
            anim.SetBool("isRunning", true); 

            
            if (moveInput > 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (moveInput < 0) transform.localScale = new Vector3(1, 1, 1);
        }
        else 
        {
            anim.SetBool("isRunning", false); 
        }
    }
}
