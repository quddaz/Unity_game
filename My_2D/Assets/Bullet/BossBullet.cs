using UnityEngine;
using UnityEngine.SceneManagement;

namespace other
{
    public class BossBullet : MonoBehaviour
    {
        public float Speed = 10f;
        public float damageRadius = 0.3f; // 피해를 입힐 범위

        private bool isGameOver = false; // 게임 오버 상태를 관리하는 변수

        private void Start()
        {
            // 생성 후 일정 시간이 지나면 총알 파괴
            Destroy(gameObject, 4f);
        }

        private void Update()
        {
            if (!isGameOver)
            {
                MoveBullet();
                DealDamageToPlayerInRange();
            }
        }

        private void MoveBullet()
        {
            transform.Translate(Vector2.right * (Speed * Time.deltaTime), Space.Self);
        }

        private void DealDamageToPlayerInRange()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToPlayer <= damageRadius)
                {
                    SceneManager.LoadScene("main ui");
                }
            }
        }
    }
}
