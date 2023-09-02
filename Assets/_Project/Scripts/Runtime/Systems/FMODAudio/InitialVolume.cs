using UnityEngine;

namespace DreamTeam.Runtime.System.FMODAudio
{
    public class InitialVolume : MonoBehaviour
    {
        public float MusicVolume;
        public float SFXVolume;
        FMOD.Studio.VCA MusicVCA;
        FMOD.Studio.VCA SFXVCA;

        void Start()
        {
            MusicVCA = FMODUnity.RuntimeManager.GetVCA(FMODKeys.VCA_Music);
            SFXVCA = FMODUnity.RuntimeManager.GetVCA(FMODKeys.VCA_SFX);
            MusicVCA.setVolume(MusicVolume);
            SFXVCA.setVolume(SFXVolume);
        }
    }
}