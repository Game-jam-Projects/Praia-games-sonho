using UnityEngine;

namespace DreamTeam.Runtime.System.FMODAudio
{
    [CreateAssetMenu(fileName = "FMOD Audio Reference", menuName = DreamKeys.ScriptablePath + "FMOD Audio Reference")]
    public class FMODAudioReferenceData : ScriptableObject
    {
        public FMODUnity.EventReference fmodPath;
    }
}