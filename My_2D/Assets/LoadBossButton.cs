using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBossButton : MonoBehaviour
{

    void Start()
    {

    }

    public void StartGame()
    {
        // 지정된 씬 로드
        SceneManager.LoadScene("Boss");
    }
    
    public void StartGame2()
    {
        // 지정된 씬 로드
        SceneManager.LoadScene("Boss2");
    }
}
