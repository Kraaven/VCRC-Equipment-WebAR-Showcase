using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class TMPToLegacyTextConverter : EditorWindow
{
    [MenuItem("Tools/Convert TMP to Legacy Text")]
    private static void ConvertTMPToLegacy()
    {
        TMP_Text[] tmpTexts = FindObjectsOfType<TMP_Text>(true);
        
        foreach (TMP_Text tmp in tmpTexts)
        {
            GameObject go = tmp.gameObject;
            string textContent = tmp.text;
            Color textColor = tmp.color;
            Font defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            float originalFontSize = tmp.fontSize;
            float scaleFactor = 0.1f; // Scale factor for visibility

            Undo.RecordObject(go, "Convert TMP to Legacy Text");

            // Remove TMP_Text component
            DestroyImmediate(tmp);

            // Add legacy Text component
            Text legacyText = go.AddComponent<Text>();
            legacyText.text = textContent;
            legacyText.color = textColor;
            legacyText.font = defaultFont;
            legacyText.fontSize = Mathf.RoundToInt(originalFontSize * 10); // Increase font size
            legacyText.alignment = TextAnchor.MiddleCenter;
            
            // Scale down the object
            go.transform.localScale *= scaleFactor;
        }

        Debug.Log("Converted all TMP_Text components to Legacy Text components.");
    }
}