using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator anim; // ลาก Animator บอสมาใส่ (ถ้าอยากให้มีท่าเจ็บ/ตาย)

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(name + " เจ็บ! เลือดเหลือ " + currentHealth);

        // ใส่ท่าเจ็บ (Optional)
        // if(anim != null) anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " ตายแล้ว!");
        // ใส่ท่าตาย (Optional)
        // if(anim != null) anim.SetBool("IsDead", true);

        // ปิดการทำงานของบอส
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BossAI>().enabled = false;
        this.enabled = false;
    }
}
