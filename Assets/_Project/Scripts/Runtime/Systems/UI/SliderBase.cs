using UnityEngine;
using UnityEngine.UI;

namespace DreamTeam.Runtime.System.UI
{
    public abstract class SliderBase : MonoBehaviour
    {
        protected Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnUpdateBeahviour);
        }

        protected abstract void OnUpdateBeahviour(float value);

    }
}