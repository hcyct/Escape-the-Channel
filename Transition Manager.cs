using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    [Header("渐变设置")]
    public Image fadeImage; // UI 渐变 Image
    public float fadeDuration = 1f; // 渐变时间

    //[Header("UI 面板")]
    private GameObject settingsPanel; // 设置面板

    private bool isPaused = false;

    private void Awake()
    {
        // 确保单例模式，防止重复创建
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        Time.timeScale = 1f; // 场景切换时确保恢复时间
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0.75f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    // 1️⃣ 暂停与恢复游戏
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log("游戏" + (isPaused ? "已暂停" : "已恢复"));
    }

    // 2️⃣ 重新开始当前关卡
    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        LoadScene(currentScene); // 调用已有 LoadScene 方法，自动带渐变效果
    }

    // 3️⃣ 返回主菜单
    public void ReturnToMainMenu()
    {
        LoadScene("StartScene"); // 替换为你的主菜单场景名
    }

    // 4️⃣ 打开/关闭设置面板
    public void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}
