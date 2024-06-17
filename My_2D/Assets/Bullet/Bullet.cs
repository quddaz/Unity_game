using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 moveDirection; // 총알의 이동 방향
    public float destroyDelay = 2f; // 총알이 생성된 후 파괴될 때까지의 지연 시간
    public float damageRadius = 1f; // 피해를 입힐 범위
    public int damageAmount = 10; // 총알에 의한 피해량

    // 방향을 설정하는 메서드
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized; // 방향 벡터를 정규화하여 저장

        // 총알의 이미지를 방향에 따라 반전시킴
        if (direction.x < 0) // 방향이 왼쪽(-x)으로 향할 때
        {
            transform.localScale = new Vector3(-1, 1, 1); // 이미지를 반전시킴
        }
        else // 방향이 오른쪽(+x)으로 향할 때
        {
            transform.localScale = new Vector3(1, 1, 1); // 이미지를 원래대로 설정
        }
    }

    private void Update()
    {
        // 총알을 설정된 방향으로 이동
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // 일정 시간이 지나면 총알 파괴
        DestroyAfterDelay();
        
        // 보스에게 피해 주기
        DealDamageToBossInRange();
    }

    private void DestroyAfterDelay()
    {
        // destroyDelay 시간 후에 자신을 파괴
        Destroy(gameObject, destroyDelay);
    }

    private void DealDamageToBossInRange()
    {
        // 보스 오브젝트 찾기
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            // 총알과 보스의 거리 계산
            float distanceToBoss = Vector3.Distance(transform.position, boss.transform.position);

            // 만약 보스와의 거리가 damageRadius 이내라면 피해를 입힘
            if (distanceToBoss <= damageRadius)
            {
                // 보스의 피 감소
                BossHealth bossHealth = boss.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(damageAmount);
                }

                // 총알 파괴
                Destroy(gameObject);
            }
        }
    }
}
