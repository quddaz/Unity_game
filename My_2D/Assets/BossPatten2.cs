using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shot
{
    public class BossPatten2 : MonoBehaviour
    {
        public float interval = 5f; // 발사 간격 (초)
        public Transform target; // 총알이 향할 대상 (타겟)
        public GameObject bulletPrefab; // 발사할 총알 프리팹
        public GameObject bulletPrefab2; // 발사할 총알 프리팹
        public Vector3 bulletSpawnOffset; // 총알 발사 위치 offset

        // 총알이 발사될 위치
        public Transform ShotPosition;


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
                int pattern = Random.Range(0, 2); // 0부터 2까지의 랜덤 정수 생성 (패턴 수에 따라 변경)
                
                // 스위치 문으로 패턴 선택
                switch (pattern)
                {
                    case 0:
                        StartCoroutine(Shoot1());
                        break;
                    case 1:
                        for (int i = 0; i < 5; i++)
                        {
                            yield return new WaitForSeconds(0.5f);
                            Shoot2();
                        }
                        break;
                    // 새로운 패턴을 추가할 경우 case 추가
                }
            }
        }

        private IEnumerator Shoot1()
        {
            for (int i = 0; i < 40; i++)
            {
                // 총알 생성
                GameObject temp = Instantiate(bulletPrefab);

                // 총알 생성 위치를 머즐 입구로 한다.
                temp.transform.position = ShotPosition.position;

                // 총알을 타겟 방향으로 회전시킴
                Vector3 direction = (target.position - temp.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                temp.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                // 총알 일정 시간 후 삭제
                Destroy(temp, 4f);

                // 0.1초 간격으로 발사
                yield return new WaitForSeconds(0.05f);
            }
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


    }
}
