using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamTeam.Runtime.System.UI
{
    public class ButtonFeedback : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private ButtonFeedbackData textMeshColor;

        public void OnSelect(BaseEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseEnterColor;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseExitColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseEnterColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            textMeshProUGUI.color = textMeshColor.OnMouseExitColor;
        }
    }
}