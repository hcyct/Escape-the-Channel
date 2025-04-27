using UnityEngine;
using UnityEngine.UI;

public class FinalResultDisplayer : MonoBehaviour
{
    public Text totalCoinsText;
    public Text totalTimeText;

    public void ShowFinalResult()
    {
        int coins1 = PlayerPrefs.GetInt("Level_01_Coins", 0);
        int coins2 = PlayerPrefs.GetInt("Level_02_Coins", 0);
        int totalCoins = coins1 + coins2;

        float time1 = PlayerPrefs.GetFloat("Level_01_Time", 0f);
        float time2 = PlayerPrefs.GetFloat("Level_02_Time", 0f);
        float totalTime = time1 + time2;

        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);

        totalCoinsText.text = $"TotalCoins: {coins2}";
        totalTimeText.text = $"TotalTime: {minutes:00}:{seconds:00}";
    }
}
