using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 7f; // ความเร็วลูกกระสุน
    public float size = 4.5f;

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
        if (hitInfo.CompareTag("Player") || hitInfo.name == "Player")
        {
            // สั่งลดเลือด
            PlayerHealth hp = hitInfo.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(20); // ลดเลือด 20 หน่วย
            }

            // ทำลายกระสุนทิ้ง (ให้หายไป)
            Destroy(gameObject);
        }
    }
}