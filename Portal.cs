using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName; // 目标场景名称

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保是玩家进入
        {
            SceneManager.LoadScene(nextSceneName); // 加载下一个场景
        }
    }
}
