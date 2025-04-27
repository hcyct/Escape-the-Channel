using UnityEngine;
using UnityEngine.UI;

public class LevelResultSaver : MonoBehaviour
{
    public string levelName = "Level_01"; // 可设置为 "Level_01" 或 "Level_02"
    public Text coinText; // 从UI中绑定 Coins: XX 文本

    private float playTime = 0f;
    private bool isTiming = true;

    void Update()
    {
        if (isTiming)
        {
            playTime += Time.deltaTime;
        }
    }

    public void SaveLevelData()
    {
        // 获取金币数量，从 Text 中解析
        int coins = ParseCoinText(coinText.text);

        // 停止计时器
        isTiming = false;

        // 保存金币和时间
        PlayerPrefs.SetInt($"{levelName}_Coins", coins);
        PlayerPrefs.SetFloat($"{levelName}_Time", playTime);

        // 保存运行次数 index（每关单独计）
        int count = PlayerPrefs.GetInt($"{levelName}_Index", 0);
        PlayerPrefs.SetInt($"{levelName}_Index", count + 1);

        PlayerPrefs.Save(); // 写入
        Debug.Log($"[{levelName}] 存档成功: Coins={coins}, Time={playTime:F2}s, Index={count + 1}");
    }

    private int ParseCoinText(string text)
    {
        // 假设格式是 "Coins: 12"
        if (text.StartsWith("Coins:"))
        {
            string numberStr = text.Substring(6).Trim();
            int.TryParse(numberStr, out int coins);
            return coins;
        }
        return 0;
    }

    // 可选：调试时强制重置数据
    [ContextMenu("Clear This Level PlayerPrefs")]
    void ClearLevelPrefs()
    {
        PlayerPrefs.DeleteKey($"{levelName}_Coins");
        PlayerPrefs.DeleteKey($"{levelName}_Time");
        PlayerPrefs.DeleteKey($"{levelName}_Index");
        PlayerPrefs.Save();
        Debug.Log($"[{levelName}] 存档数据已清空");
    }
}
