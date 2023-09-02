using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamTeam.Runtime.System.UI
{
    public abstract class ButtonFeedbackBase : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public abstract void OnDeselect(BaseEventData eventData);
        public abstract void OnPointerEnter(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);
        public abstract void OnSelect(BaseEventData eventData);
    }
}