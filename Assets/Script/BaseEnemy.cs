using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("ค่าพื้นฐาน (จากตัวแม่)")]
    public int maxHealth = 100;

    [SerializeField]
    protected int currentHealth; 

    protected Animator anim;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(name + " โดนตี! เลือดเหลือ " + currentHealth);

        if (anim != null) anim.SetTrigger("Hurt"); 

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }
    protected abstract void Die();
}
