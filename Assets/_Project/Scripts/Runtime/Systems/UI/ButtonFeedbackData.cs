using UnityEngine;

namespace DreamTeam.Runtime.System.UI
{
    [CreateAssetMenu(fileName = "New Button Feedback", menuName = DreamKeys.ScriptablePath + "Button Feedback")]
    public class ButtonFeedbackData : ScriptableObject
    {
        public Color OnMouseEnterColor = Color.gray;
        public Color OnMouseExitColor = Color.white;
    }
}