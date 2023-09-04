using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DreamTeam.Runtime.System.UI
{
    public class UiDetectGamepad : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        [SerializeField] private GameObject keyboard;
        [SerializeField] private GameObject xbox;
        [SerializeField] private GameObject ps;

        private InputDevice _lastDevice;

        [SerializeField] private AutoTimer timer;

        private void Start()
        {
            Desactive();
        }

        private void OnEnable()
        {
            timer.Start(timer.InitTime);
            timer.OnExpire += () =>
            {
                Active();
            };

            InputSystem.onActionChange += (obj, change) =>
            {
                if (change == InputActionChange.ActionPerformed)
                {
                    var inputAction = (InputAction)obj;
                    var lastControl = inputAction.activeControl;
                    var lastDevice = lastControl.device;

                    if (lastDevice == _lastDevice)
                        return;

                    DisableAll();

                    var alo = lastDevice.displayName.ToLower();

                    if (alo.Contains("keyboard") || alo.Contains("mouse"))
                    {
                        keyboard.SetActive(true);
                    }

                    if (alo.Contains("xbox"))
                    {
                        xbox.SetActive(true);
                    }

                    if (alo.Contains("playstation") || alo.Contains("ps"))
                    {
                        ps.SetActive(true);
                    }

                    _lastDevice = lastDevice;
                }
            };
        }

        private void OnDisable()
        {
            timer.Kill();
        }

        private void DisableAll()
        {
            keyboard.SetActive(false);
            xbox.SetActive(false);
            ps.SetActive(false);
        }

        public void Active()
        {
            canvas.SetActive(true);
        }

        public void Desactive()
        {
            canvas.SetActive(false);
        }
    }
}