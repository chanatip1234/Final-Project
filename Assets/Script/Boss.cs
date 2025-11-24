using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("ค่าพลังต่างๆ")]
    public float speed = 2f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    [Header("สิ่งที่ต้องใส่")]
    public Transform player;
    // ไม่ต้องใช้ Wall Left/Right ในการเช็คระยะแล้ว แต่ปล่อยคาไว้ได้ ไม่เป็นไรครับ

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
        // 1. เช็คระยะห่างผู้เล่น
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            AttackBehavior(); // เจอตัว -> ตี
        }
        else
        {
            PatrolBehavior(); // ไม่เจอ -> เดิน
        }
    }

    void PatrolBehavior()
    {
        // เดินซ้าย-ขวา ตามปกติ (ลบส่วนเช็ค Position ออกแล้ว)
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
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // หยุดเดิน

        // หันหน้าหาผู้เล่น
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // โจมตีตามรอบ
        if (Time.time >= nextAttackTime)
        {
            if (anim != null) anim.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    // --- เพิ่มฟังก์ชันนี้: ถ้าเดินชนกำแพง ให้กลับหลังหัน ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveRight = !moveRight; // สลับทิศทันทีที่ชน
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
