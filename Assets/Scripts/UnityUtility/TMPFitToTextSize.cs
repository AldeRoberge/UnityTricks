using TMPro;
using UnityEngine;

namespace UnityUtility
{
    /// <summary>
    /// Allows the RectTransform to fit to the size of the Text.
    /// </summary>
    public class TMPFitToTextSize : MonoBehaviour
    {
        public TextMeshProUGUI textObj;
        public RectTransform toScale;
        
        public float Padding = 0.25f;

        void Start()
        {
            UpdateWidth(textObj.text);

            textObj.OnChange(UpdateWidth);
        }

        private void UpdateWidth(string text)
        {
            float width = (textObj.GetRenderedValues(true).x);
            toScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width + Padding);
        }
    }
}