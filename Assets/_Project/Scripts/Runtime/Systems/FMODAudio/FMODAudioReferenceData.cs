using UnityEngine;

namespace DreamTeam.Runtime.System.FMODAudio
{
    [CreateAssetMenu(fileName = "FMOD Audio Reference", menuName = DreamKeys.ScriptablePath)]
    public class FMODAudioReferenceData : ScriptableObject
    {
        public FMODUnity.EventReference fmodPath;
    }
}