using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamTeam.Runtime.System.UI
{
    public class ButtonFeedback : ButtonFeedbackBase
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private ButtonFeedbackData textMeshColor;

        public override void OnSelect(BaseEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseEnterColor;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseExitColor;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseEnterColor;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseExitColor;
        }
    }
}