using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : BaseEnemy
{
    [Header("ส่วนของ UI")]
    public GameObject bossUI;   
    public Slider healthSlider;
    public Image fillImage;

    [Header("การเช็คระยะ")]
    public Transform player;    
    public float showRange = 10f; 

    
    protected override void Start()
    {
        base.Start(); 

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; 
            healthSlider.value = maxHealth;
        }

        if (bossUI != null) bossUI.SetActive(false); 
    }

    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < showRange)
        {
            if (bossUI != null) bossUI.SetActive(true);

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;

                if (fillImage != null)
                {
                    fillImage.enabled = currentHealth > 0;
                }
            }
        }
        else
        {
            if (bossUI != null) bossUI.SetActive(false);
        }
    }

    protected override void Die()
    {
        Debug.Log("บอสผู้ยิ่งใหญ่ล้มลงแล้ว!");

        
        if (bossUI != null) bossUI.SetActive(false);

        if (anim != null) anim.SetBool("IsDead", true);

        if (GetComponent<BossAI>() != null)
            GetComponent<BossAI>().enabled = false;

        GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Destroy(gameObject, 5f);
    }
}
