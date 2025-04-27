using UnityEngine;
using UnityEngine.UI;

public class LevelResultSaver : MonoBehaviour
{
    public string levelName = "Level_01"; // ������Ϊ "Level_01" �� "Level_02"
    public Text coinText; // ��UI�а� Coins: XX �ı�

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
        // ��ȡ����������� Text �н���
        int coins = ParseCoinText(coinText.text);

        // ֹͣ��ʱ��
        isTiming = false;

        // �����Һ�ʱ��
        PlayerPrefs.SetInt($"{levelName}_Coins", coins);
        PlayerPrefs.SetFloat($"{levelName}_Time", playTime);

        // �������д��� index��ÿ�ص����ƣ�
        int count = PlayerPrefs.GetInt($"{levelName}_Index", 0);
        PlayerPrefs.SetInt($"{levelName}_Index", count + 1);

        PlayerPrefs.Save(); // д��
        Debug.Log($"[{levelName}] �浵�ɹ�: Coins={coins}, Time={playTime:F2}s, Index={count + 1}");
    }

    private int ParseCoinText(string text)
    {
        // �����ʽ�� "Coins: 12"
        if (text.StartsWith("Coins:"))
        {
            string numberStr = text.Substring(6).Trim();
            int.TryParse(numberStr, out int coins);
            return coins;
        }
        return 0;
    }

    // ��ѡ������ʱǿ����������
    [ContextMenu("Clear This Level PlayerPrefs")]
    void ClearLevelPrefs()
    {
        PlayerPrefs.DeleteKey($"{levelName}_Coins");
        PlayerPrefs.DeleteKey($"{levelName}_Time");
        PlayerPrefs.DeleteKey($"{levelName}_Index");
        PlayerPrefs.Save();
        Debug.Log($"[{levelName}] �浵���������");
    }
}
