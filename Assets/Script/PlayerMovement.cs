using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Dash Settings")]
    public float dashPower = 25f;   // ลองปรับเลขนี้ให้เยอะขึ้นถ้ายังพุ่งไม่แรง
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("I-Frame Settings")]
    public int playerLayer = 6;
    public int projectileLayer = 8;

    private Rigidbody2D rb;
    private Animator anim;

    // 🔥 แก้ 1: เปลี่ยนจากตัวเดียว เป็น Array (เก็บหลายชิ้น)
    private SpriteRenderer[] srs;

    private bool isGrounded;
    private bool isDashing;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        // 🔥 แก้ 1: ดึง SpriteRenderer ทั้งหมดในตัวลูกๆ มาเก็บไว้
        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isDashing) return;

        // กด Shift ซ้าย เพื่อพุ่ง
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(DashRoutine());
        }

        // --- เดินปกติ ---
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            if (anim != null) anim.SetBool("isRunning", true);

            // กลับด้าน (Logic ของคุณ: -1=ขวา, 1=ซ้าย)
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

    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // เริ่มอมตะ
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, true);
        if (srs != null) foreach (SpriteRenderer sr in srs) sr.color = new Color(1f, 1f, 1f, 0.5f);

        // คำนวณทิศทาง
        float direction = (transform.localScale.x < 0) ? 1f : -1f;

        // -------------------------------------------------------------
        // 🔥 แก้ไขใหม่: ใช้ Loop บังคับความเร็ว "ทุกเฟรม" ตลอดการพุ่ง
        // -------------------------------------------------------------
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            // บังคับความเร็วเลย ไม่สนแรงเสียดทานหน้าไหนทั้งนั้น
            rb.linearVelocity = new Vector2(direction * dashPower, 0f);

            yield return null; // รอเฟรมถัดไปแล้วทำซ้ำ
        }
        // -------------------------------------------------------------

        // จบอมตะ
        Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);
        if (srs != null) foreach (SpriteRenderer sr in srs) sr.color = Color.white;

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero; // หยุดทันทีเมื่อจบ
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}