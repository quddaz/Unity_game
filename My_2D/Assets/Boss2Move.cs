using UnityEngine;

public class Boss2Move : MonoBehaviour
{
    public float speed = 10f; // 움직임 속도
    private Rigidbody2D rb2d; // Rigidbody2D 컴포넌트에 대한 참조
    private int moveDirection = 1; // 현재 움직이는 방향 (1: 오른쪽, -1: 왼쪽)

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // 충돌 감지 모드 설정
        rb2d.velocity = new Vector2(speed * moveDirection, 0f); // 초기 속도 설정
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 현재 방향 반대로 설정
            moveDirection *= -1;

            // 속도 갱신
            rb2d.velocity = new Vector2(speed * moveDirection, 0f);

            Debug.Log("Collision detected with: " + collision.gameObject.name);
        }
    }
}
