using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    void Start()
    {
        // สั่งทำลายตัวเองใน 0.5 วินาที (ระเบิดเล่นแป๊บเดียวก็จบ)
        Destroy(gameObject, 0.5f);
    }
}