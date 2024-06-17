using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    public float jumpTimeLimit = 0.5f; // 최대 점프 시간 제한
    public float doubleJumpForce = 7f; // 2단 점프 힘

    private Rigidbody2D rb;
    private Animator animator; // Animator 컴포넌트
    private bool isJumping = false;
    private bool canJump = true; // 현재 점프 가능한 상태인지 여부
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private int jumpCount = 0; // 현재 점프 횟수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 컴포넌트를 찾을 수 없습니다!");
        }
        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (rb == null)
            return; // Rigidbody2D가 없으면 Update 함수 종료

        // 좌우 이동
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 캐릭터 방향 전환
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // 오른쪽을 보도록 설정
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f); // 왼쪽을 보도록 설정
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((isGrounded || canJump) && jumpCount < 2)
            {
                if (!isJumping) // 처음 점프 시
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isJumping = true;
                    jumpCount++;

                    // 점프 애니메이션 트리거 활성화
                    animator.SetTrigger("Jump");
                }
                else // 2단 점프 시
                {
                    rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                    jumpCount++;

                    // 2단 점프 애니메이션 트리거 활성화
                    animator.SetTrigger("Jump");
                }
            }
        }

        // 점프 중 높이 조절
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeLimit > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeLimit -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // 점프 중단
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpTimeLimit = 0.5f; // 점프 시간 초기화
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿으면 점프 관련 상태 초기화
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            canJump = true;
            isGrounded = true;
            jumpCount = 0; // 점프 횟수 초기화

            // 땅에 닿았을 때 점프 애니메이션 트리거 비활성화
            animator.ResetTrigger("Jump");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어지면 점프 관련 상태 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
