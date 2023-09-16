using UnityEngine;
using UnityEngine.UI;

namespace DreamTeam.Runtime.Systems.UI
{
    public abstract class SliderBase : MonoBehaviour
    {
        protected Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnUpdateBeahviour);
        }

        protected abstract void OnUpdateBeahviour(float value);

    }
}