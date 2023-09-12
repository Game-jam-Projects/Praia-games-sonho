using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DreamTeam.Runtime.Systems.UI
{
    public class SliderFeedback : ButtonFeedbackBase
    {
        [SerializeField] private ButtonFeedbackData feedbackData;
        [SerializeField] private Image handleImage;

        public override void OnDeselect(BaseEventData eventData)
        {
            handleImage.color = feedbackData.OnMouseExitColor;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            handleImage.color = feedbackData.OnMouseEnterColor;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            handleImage.color = feedbackData.OnMouseExitColor;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            handleImage.color = feedbackData.OnMouseEnterColor;
        }
    }
}