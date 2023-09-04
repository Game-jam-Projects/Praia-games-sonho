using DreamTeam.Runtime.System.UI;

namespace DreamTeam.Runtime.System.FMODAudio
{
    public class VCASlider : SliderBase
    {
        FMOD.Studio.VCA vca;
        public string VCAName;

        protected override void Awake()
        {
            base.Awake();

            vca = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCAName);

            vca.getVolume(out float newVolume);

            slider.value = newVolume;
        }

        protected override void OnUpdateBeahviour(float value)
        {
            vca.setVolume(value);
        }
    }
}