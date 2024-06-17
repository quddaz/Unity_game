using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shot
{
    public class CircleShotGotoShot : MonoBehaviour
    {
        // 총알을 생성 후 Target에게 날아갈 변수
        public Transform Target;

        // 발사될 총알 프리팹
        public GameObject BulletPrefab;

        // 총알 발사 위치를 설정할 프리팹의 로컬 좌표
        public Vector3 BulletSpawnOffset;


        public void Shot()
        {
            // Target 방향으로 발사될 총알 수록
            List<Transform> bullets = new List<Transform>();

            // 360도를 기준으로 원형으로 총알 발사
            for (int i = 0; i < 360; i += 13)
            {
                // 총알 생성
                GameObject temp = Instantiate(BulletPrefab);

                // 일정 시간 후 삭제
                Destroy(temp, 4f);

                // 프리팹의 로컬 좌표를 부모 객체의 위치를 기준으로 설정하여 발사 위치 조정
                temp.transform.position = transform.position + transform.TransformDirection(BulletSpawnOffset);

                // Z 값이 변해야 회전이 이루어지므로, Z에 i를 대입
                temp.transform.rotation = Quaternion.Euler(0, 0, i);

                // 발사된 총알 수록
                bullets.Add(temp.transform);
            }

            // 총알을 Target 방향으로 이동시킴
            StartCoroutine(BulletToTarget(bullets));
        }

        private IEnumerator BulletToTarget(List<Transform> objects)
        {
            // 0.5초 후에 시작
            yield return new WaitForSeconds(0.5f);

            // 모든 총알에 대해 대상(Target)을 향하도록 회전
            foreach (Transform bullet in objects)
            {
                Vector3 targetDirection = (Target.position - bullet.position).normalized;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                bullet.rotation = Quaternion.Euler(0, 0, angle);
            }

            // 데이터 해제
            objects.Clear();
        }
    }
}
