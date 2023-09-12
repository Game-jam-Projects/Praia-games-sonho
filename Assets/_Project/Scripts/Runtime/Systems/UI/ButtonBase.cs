using UnityEngine;
using UnityEngine.UI;

namespace DreamTeam.Runtime.Systems.UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ButtonBehaviour);
        }
        protected abstract void ButtonBehaviour();
    }
}