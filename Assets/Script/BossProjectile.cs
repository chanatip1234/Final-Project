using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 7f; // ความเร็วลูกกระสุน
    public float size = 4f;
    public GameObject explosionEffect;
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        Destroy(gameObject, 4f);
    }
    void LateUpdate()
    {
        transform.localScale = new Vector3(size, size, 1f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Ground"))
        {
            
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if (hitInfo.CompareTag("Player") || hitInfo.name == "Player")
            {
                PlayerHealth hp = hitInfo.GetComponent<PlayerHealth>();
                if (hp != null) hp.TakeDamage(20);
            }

            //ทำลายกระสุนทิ้งทันที
            Destroy(gameObject);
        }
    }
}