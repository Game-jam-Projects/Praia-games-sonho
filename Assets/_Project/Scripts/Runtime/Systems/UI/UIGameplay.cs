using DreamTeam.Runtime.System.Core;
using DreamTeam.Runtime.Systems.Health;
using UnityEngine;
using UnityEngine.UI;

namespace DreamTeam.Runtime.System.UI
{
    public class UIGameplay : MonoBehaviour
    {
        [SerializeField] private Image playerHealthBar;
        [SerializeField] private Image playerManaBar;

        [SerializeField] private GameObject hud;

        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameoverPanel;
        [SerializeField] private GameObject gamewinPanel;

        [SerializeField] private Button firstButtonPause;
        [SerializeField] private Button firstButtonGameOver;
        [SerializeField] private Button firstButtonGameWin;

        private HealthSystem healthSystem;
        private PlayerController playerController;

        private void Awake()
        {
             playerController = FindObjectOfType<PlayerController>();
             healthSystem = playerController.GetComponent<HealthSystem>();
        }

        private void Start()
        {
            healthSystem.OnChangeHealth += UpdateLifeBar;

            GameManager.Instance.OnPauseStatusChange += UpdatePauseMenu;
            GameManager.Instance.OnGameOver += OpenGameoverMenu;
            GameManager.Instance.OnGameWin += OpenGamewinMenu;
        }

        private void OnDestroy()
        {
            healthSystem.OnChangeHealth -= UpdateLifeBar;

            GameManager.Instance.OnPauseStatusChange -= UpdatePauseMenu;
            GameManager.Instance.OnGameOver -= OpenGameoverMenu;
            GameManager.Instance.OnGameWin -= OpenGamewinMenu;
        }

        private void UpdateLifeBar(HealthArgs healthArgs)
        {
            playerHealthBar.fillAmount = healthArgs.current / healthArgs.max;
        }

        private void UpdatePauseMenu(bool value)
        {
            DisableAllMenus();

            firstButtonPause.Select();
            pausePanel.SetActive(value);
        }

        private void OpenGameoverMenu()
        {
            DisableAllMenus();

            firstButtonGameOver.Select();
            gameoverPanel.SetActive(true);
        }

        private void OpenGamewinMenu()
        {
            DisableAllMenus();

            firstButtonGameWin.Select();
            gamewinPanel.SetActive(true);
        }

        private void DisableAllMenus()
        {
            pausePanel.SetActive(false);
            gameoverPanel.SetActive(false);
        }

        private void DisableHUD()
        {
            DisableAllMenus();
            hud.SetActive(false);
        }
    }
}