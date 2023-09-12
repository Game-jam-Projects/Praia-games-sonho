using UnityEngine;

namespace DreamTeam.Runtime.Systems.FMODAudio
{
    public class AnimationOneShotSound : MonoBehaviour
    {
        public void PlayEvent(string path)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
        }
    }
}