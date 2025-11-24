using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;   // ลากจุด AttackPoint มาใส่
    public float attackRange = 0.5f; // รัศมีวงดาบ
    public LayerMask enemyLayers;   // เลือก Layer "Enemies"
    public int attackDamage = 40;   // พลังโจมตี

    void Update()
    {
        // ถ้าคลิกเมาส์ซ้าย (หรือเปลี่ยนเป็น KeyCode.Z ก็ได้)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. เล่นท่าฟัน
        if (anim != null) anim.SetTrigger("Attack");

        // 2. ตรวจจับศัตรูในวงดาบ
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // 3. สั่งลดเลือดศัตรูทุกตัวที่โดน
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ฟันโดน " + enemy.name);
            // เดี๋ยวเราจะมาเขียนสคริปต์ EnemyHealth กันต่อในขั้นตอนหน้า
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    // วาดวงกลมสีแดงให้เห็นในหน้าจอ Scene (ไว้กะระยะ)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
