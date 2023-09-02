using DreamTeam.Runtime.System.UI;

namespace DreamTeam.Runtime.System.FMODAudio
{
    public class VCASlider : SliderBase
    {
        FMOD.Studio.VCA vca;
        public string VCAName;

        private void Awake()
        {
            vca = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCAName);
        }

        protected override void OnUpdateBeahviour(float value)
        {
            vca.setVolume(value);
        }
    }
}