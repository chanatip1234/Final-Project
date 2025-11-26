using UnityEngine;
public class SkeletonAI : MonoBehaviour
{
    [Header("ค่าพลัง")]
    public float speed = 3f;            
    public float attackRange = 1.5f;    
    public float attackCooldown = 1.5f; 
    public int damage = 10;             

    [Header("ใส่ตัวผู้เล่น")]
    public Transform player;            

    
    private Rigidbody2D rb;
    private Animator anim;
    private bool moveRight = true;      
    private float nextAttackTime = 0f;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponentInChildren<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < attackRange)
        {
            AttackState(); 
        }
        else
        {
            PatrolState(); 
        }
    }

    void PatrolState()
    {
        
        if (anim != null) anim.SetBool("isRunning", true);

        if (moveRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void AttackState()
    {
        
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        
        if (anim != null) anim.SetBool("isRunning", false);

        
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        
        if (Time.time >= nextAttackTime)
        {
            if (anim != null) anim.SetTrigger("Attack"); 

            
            if (player != null)
            {
                PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
                if (pHealth != null)
                {
                    pHealth.TakeDamage(damage);
                }
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("EnemyWall"))
        {
            moveRight = !moveRight; 
        }
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
