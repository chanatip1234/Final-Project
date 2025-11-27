using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 7f; // ความเร็วลูกกระสุน
    public float size = 3f;

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
        // เช็คว่าชนผู้เล่นไหม
        if (hitInfo.name == "Player" || hitInfo.CompareTag("Player"))
        {
            Debug.Log("โดนผู้เล่น!");
            Destroy(gameObject); // ชนแล้วหายไป
        }
    }
}