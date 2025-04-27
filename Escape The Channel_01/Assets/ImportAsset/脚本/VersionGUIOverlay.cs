using UnityEngine;

public class VersionGUIOverlay : MonoBehaviour
{
    private GUIStyle style;

    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.UpperRight;
    }

    void OnGUI()
    {
        Rect rect = new Rect(Screen.width - 220, 10, 200, 40);
        GUI.Label(rect, "Version: " + Application.version, style);
    }
}
