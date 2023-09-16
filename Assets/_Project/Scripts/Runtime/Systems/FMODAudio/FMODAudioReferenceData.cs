using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.FMODAudio
{
    [CreateAssetMenu(fileName = "FMOD Audio Reference", menuName = DreamKeys.ScriptablePath + "FMOD Audio Reference")]
    public class FMODAudioReferenceData : ScriptableObject
    {
        public FMODUnity.EventReference fmodPath;
    }
}