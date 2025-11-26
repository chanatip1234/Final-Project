using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifeTime = 5f; 

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        else if (hitInfo.gameObject.CompareTag("Ground") || hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
