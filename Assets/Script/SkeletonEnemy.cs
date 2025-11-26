using UnityEngine;

public class SkeletonEnemy : BaseEnemy
{
    protected override void Die()
    {
        Debug.Log("กระดูกแตกตายแล้วจ้า!");

        
        if (anim != null) anim.SetBool("IsDead", true);

        
        if (GetComponent<SkeletonAI>() != null)
            GetComponent<SkeletonAI>().enabled = false;

        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        Destroy(gameObject, 2f); 
    }
}
