using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatten : MonoBehaviour
{
    public float interval = 5f; // 발사 간격 (초)
    public Transform target; // 총알이 향할 대상 (타겟)
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public GameObject bulletPrefab2; // 발사할 총알 프리팹
    public GameObject bulletPrefab3; // 발사할 총알 프리팹 (Shoot3용)
    public Vector3 bulletSpawnOffset; // 총알 발사 위치 offset

    void Start()
    {
        // ShootPattern 코루틴 시작
        StartCoroutine(ShootPattern());
    }

    private IEnumerator ShootPattern()
    {
        while (true)
        {
            // interval 간격으로 발사
            yield return new WaitForSeconds(interval);

            // 랜덤으로 패턴 선택
            int pattern = Random.Range(0, 3); // 0부터 2까지의 랜덤 정수 생성 (패턴 수에 따라 변경)
            
            // 스위치 문으로 패턴 선택
            switch (pattern)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        yield return new WaitForSeconds(1f);
                        Shoot1();
                    }
                    break;
                case 1:
                    for (int i = 0; i < 8; i++)
                    {
                        yield return new WaitForSeconds(0.5f);
                        Shoot2();
                    }
                    break;
                case 2:
                    for (int i = 0; i < 6; i++)
                    {
                        yield return new WaitForSeconds(1f);
                        Shoot3();
                    }
                    break;
                // 새로운 패턴을 추가할 경우 case 추가
            }
        }
    }

    private void Shoot1()
    {
        // 총알 발사될 방향 리스트
        List<GameObject> bullets = new List<GameObject>();

        // 360도를 기준으로 원형으로 총알 발사
        for (int i = 0; i < 360; i += 13)
        {
            // 총알 생성
            GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.TransformDirection(bulletSpawnOffset), Quaternion.identity);

            // 총알 발사 각도 설정
            newBullet.transform.rotation = Quaternion.Euler(0f, 0f, i);

            // 발사된 총알 리스트에 추가
            bullets.Add(newBullet);

            // 총알 일정 시간 후 삭제
            Destroy(newBullet, 4f);
        }

        // 총알을 타겟 방향으로 이동시키는 코루틴 시작
        StartCoroutine(MoveBulletsToTarget(bullets));
    }

    private void Shoot2()
    {
        // 360도를 기준으로 원형으로 총알 발사
        for (int i = 0; i < 360; i += 13)
        {
            // 총알 생성
            GameObject newBullet = Instantiate(bulletPrefab2, transform.position + transform.TransformDirection(bulletSpawnOffset), Quaternion.identity);

            // 총알 발사 각도 설정
            newBullet.transform.rotation = Quaternion.Euler(0f, 0f, i);

            // 총알 일정 시간 후 삭제
            Destroy(newBullet, 4f);
        }
    }

    private void Shoot3()
    {
        float startY = -5.5f;
        float endY = 5.5f;

        for (float y = startY; y <= endY; y += 0.5f) // 1.5f는 총알 간격, 필요에 따라 조정 가능
        {
            // 일정 확률로 빈 공간 만들기
            if (Random.value > 0.5f)
            {
                continue;
            }

            // 총알 생성
            Vector3 spawnPosition = new Vector3(10, y, 0) + bulletSpawnOffset;
            GameObject newBullet = Instantiate(bulletPrefab3, spawnPosition, Quaternion.identity);

            // 위쪽으로 이동하도록 방향 설정
            newBullet.transform.rotation = Quaternion.Euler(0f, 0f, 180);

            // 총알 일정 시간 후 삭제
            Destroy(newBullet, 7f);
        }
    }

    private IEnumerator MoveBulletsToTarget(List<GameObject> bullets)
    {
        yield return new WaitForSeconds(0.5f); // 발사 후 일정 시간 대기

        // 모든 총알을 타겟 방향으로 회전
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null && target != null)
            {
                Vector3 direction = (target.position - bullet.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}
