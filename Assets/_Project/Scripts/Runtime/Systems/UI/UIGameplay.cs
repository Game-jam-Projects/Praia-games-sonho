using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Systems.Health;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DreamTeam.Runtime.Systems.UI
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

        [Header("HUD")]
        public TMP_Text tradeDreamAmount;
        public TMP_Text tradeDreamShadow;

        [Header("Debug")]
        public TMP_Text timer;
        public TMP_Text itens;
        public TMP_Text mortes;

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
            GameManager.Instance.OnUpdateSwapItemAmount += OnUpdateSwapItemAmount;
        }

        private void OnDestroy()
        {
            healthSystem.OnChangeHealth -= UpdateLifeBar;

            GameManager.Instance.OnPauseStatusChange -= UpdatePauseMenu;
            GameManager.Instance.OnGameOver -= OpenGameoverMenu;
            GameManager.Instance.OnGameWin -= OpenGamewinMenu;
            GameManager.Instance.OnUpdateSwapItemAmount -= OnUpdateSwapItemAmount;
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

        private void OnUpdateSwapItemAmount(int value)
        {
            tradeDreamAmount.text = value.ToString();
            tradeDreamShadow.text = value.ToString();
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

        private void LateUpdate()
        {
            timer.text = "Tempo: " + CoreSingleton.Instance.gameManager.chrono.GetElapsedTime().ToString("N0");
            itens.text = "Itens: " + CoreSingleton.Instance.gameManager.GetDreamAcount().ToString();
            mortes.text = "Mortes: " + CoreSingleton.Instance.gameManager.GetDeathCount().ToString();
        }
    }
}