using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("ค่าพลังต่างๆ")]
    public float speed = 2f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public int damage = 20;

    [Header("สิ่งที่ต้องใส่")]
    public Transform player;
    

    private Rigidbody2D rb;
    private Animator anim;
    private bool moveRight = true;
    private float nextAttackTime = 0f;
    private Vector3 originalScale;

    [Header("Projectile Settings")] 
    public Transform firePoint;     
    public GameObject bulletPrefab; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            AttackBehavior(); 
        }
        else
        {
            PatrolBehavior(); 
        }
    }

    void PatrolBehavior()
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

    void AttackBehavior()
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
           
            /*PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }*/
               
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveRight = !moveRight;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Quaternion bulletRotation = Quaternion.identity;

            // เช็คเลยว่า "ผู้เล่นอยู่ทางซ้ายของบอสไหม?"
            if (player.position.x < transform.position.x)
            {
                // ถ้าอยู่ซ้าย -> หมุนกระสุน 180 องศา
                bulletRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // ถ้าอยู่ขวา -> ไม่ต้องหมุน (0 องศา)
                bulletRotation = Quaternion.Euler(0, 0, 0);
            }

            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }
    }

}
