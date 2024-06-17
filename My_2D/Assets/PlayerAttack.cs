using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 위치
    public float fireRate = 0.5f; // 발사 간격 설정

    private float nextFireTime = 0f; // 다음 발사 시간 저장 변수

    // Update is called once per frame
    void Update()
    {
        // 'Z' 키가 눌렸을 때 총알 발사
        if (Input.GetKey(KeyCode.Z) && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate; // 다음 발사 시간 갱신
        }
    }

    // 총알 발사 메서드
    void FireBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // 플레이어가 바라보는 방향을 계산
            Vector3 playerDirection = transform.right; // 현재는 플레이어의 오른쪽 방향으로 설정되어 있음

            // 총알을 발사하는 방향을 플레이어가 바라보는 방향으로 변경
            if (transform.localScale.x < 0) // 플레이어가 왼쪽을 바라볼 때
            {
                playerDirection = -transform.right; // 왼쪽을 바라보는 방향으로 변경
            }
            
            // 총알 프리팹을 발사 위치에서 생성
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // 생성된 총알의 방향 설정
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                // 플레이어가 바라보는 방향을 총알의 방향으로 설정
                bulletComponent.SetDirection(playerDirection);
            }
        }
    }
}