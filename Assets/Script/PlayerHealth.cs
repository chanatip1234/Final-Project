using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("ส่วนของ UI")]
    public Slider healthSlider; 
    public Image fillImage;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
        if (fillImage != null) fillImage.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("โอ๊ย! โดนตี เลือดเหลือ: " + currentHealth);

        
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;

            if (fillImage != null)
            {
                fillImage.enabled = currentHealth > 0;
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ผู้เล่นตายแล้ว...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
