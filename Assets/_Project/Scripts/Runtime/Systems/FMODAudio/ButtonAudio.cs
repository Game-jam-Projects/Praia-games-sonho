using DreamTeam.Runtime.System.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamTeam.Runtime.System.FMODAudio
{
    public class ButtonAudio : ButtonBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private FMODAudioReferenceData Click;
        [SerializeField] private FMODAudioReferenceData HoverIn;
        [SerializeField] private FMODAudioReferenceData HoverOut;

        [SerializeField] private bool disableClickByScript;

        public void ClickSound()
        {
            if(Click)
                FMODUnity.RuntimeManager.PlayOneShotAttached(Click.fmodPath, gameObject);
        }
        public void HoverInSound()
        {
            if(HoverIn)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverIn.fmodPath, gameObject);
        }
        public void HoverOutSound()
        {
            if(HoverOut)
                FMODUnity.RuntimeManager.PlayOneShotAttached(HoverOut.fmodPath, gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HoverInSound();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HoverOutSound();
        }

        protected override void ButtonBehaviour()
        {
            if(disableClickByScript)
                ClickSound();
        }
    }
}