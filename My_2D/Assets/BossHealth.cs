using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Vector3 BulletSpawnOffset;
    public int maxHealth = 200;
    private int currentHealth;
    public UnityAction OnHealthChanged;
    public Slider healthSlider;
    private BossMove bossMove; // BossMove 스크립트의 참조

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GameObject.FindWithTag("BossHealthSlider").GetComponent<Slider>();
        bossMove = GetComponent<BossMove>(); // BossMove 스크립트를 찾아서 할당
        UpdateHealthSlider();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke();
        }
        UpdateHealthSlider();
    }

    private void Die()
    {
        // 움직임 멈추기
        if (bossMove != null)
        {
            bossMove.StopMovement();
        }

        InvokeRepeating("Shoot", 0f, 0.1f); // Shoot 메서드를 0.1초 간격으로 반복 실행
        Invoke("LoadMainUIScene", 2f);
    }

    private void UpdateHealthSlider()
    {
        float healthRatio = (float)currentHealth / maxHealth;
        healthSlider.value = healthRatio;
    }
    private void LoadMainUIScene()
    {
        SceneManager.LoadScene("main ui");
    }
    private void Shoot()
    {
        GameObject temp = Instantiate(BulletPrefab);
        temp.transform.position = transform.position + transform.TransformDirection(BulletSpawnOffset);
        float randomZRotation = Random.Range(0f, 360f);
        temp.transform.rotation = Quaternion.Euler(0f, 0f, randomZRotation);
        Destroy(temp, 0.5f); // 총알 생성 후 0.5초 후에 삭제
    }
}
