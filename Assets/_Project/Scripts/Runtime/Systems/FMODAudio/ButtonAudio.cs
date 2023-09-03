using DreamTeam.Runtime.System.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamTeam.Runtime.System.FMODAudio
{
    public class ButtonAudio : ButtonBase, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private FMODAudioReferenceData Click;
        [SerializeField] private FMODAudioReferenceData HoverIn;
        [SerializeField] private FMODAudioReferenceData HoverOut;

        [SerializeField] private bool disableClickByScript;
        [SerializeField] private bool disableHoverOut = true;

        public void ClickSound()
        {
            if (Click)
                FMODUnity.RuntimeManager.PlayOneShotAttached(Click.fmodPath, gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (HoverIn)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverIn.fmodPath, gameObject);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if(disableHoverOut)
                    return;

            if (HoverOut)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverOut.fmodPath, gameObject);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (HoverIn)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverIn.fmodPath, gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (disableHoverOut)
                return;

            if (HoverOut)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverOut.fmodPath, gameObject);
        }

        protected override void ButtonBehaviour()
        {
            if (disableClickByScript)
                ClickSound();
        }
    }
}