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
            float scaleFactor = 1000; // Scale factor for visibility

            Undo.RecordObject(go, "Convert TMP to Legacy Text");

            // Remove TMP_Text component
            DestroyImmediate(tmp);

            // Add legacy Text component
            Text legacyText = go.AddComponent<Text>();
            legacyText.text = textContent;
            legacyText.color = textColor;
            legacyText.font = defaultFont;
            legacyText.fontSize = (int) (originalFontSize * scaleFactor);
            legacyText.alignment = TextAnchor.MiddleCenter;
            legacyText.horizontalOverflow = HorizontalWrapMode.Overflow;
            legacyText.verticalOverflow = VerticalWrapMode.Overflow;
            
            // Scale down the object
            go.transform.localScale /= scaleFactor;
        }

        Debug.Log("Converted all TMP_Text components to Legacy Text components.");
    }
}