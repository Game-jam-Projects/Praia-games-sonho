using DreamTeam.Runtime.System.Ranking;
using System;
using UnityEngine;

namespace DreamTeam.Runtime.System.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action<bool> OnPauseStatusChange;
        public event Action OnGameOver;
        public event Action OnGameWin;

        public bool Paused { get; private set; }


        [Header("Ranking")]
        [SerializeField] private int collectableDreams;
        [SerializeField] private int deathCount;
        public PlayerDataRanking playerDataRanking;
        public Chrono chrono;

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            playerDataRanking = new PlayerDataRanking();
            chrono = new Chrono();
        }

        /// <summary>
        /// Use pra evitar que o player pause em horas que nao deve.
        /// </summary>
        public bool CanPause = true;

        private void PauseGame()
        {
            Paused = true;
            chrono.Stop();
            Time.timeScale = 0;
            OnPauseStatusChange?.Invoke(Paused);
        }

        public void ResumeGame()
        {
            Paused = false;
            chrono.Start();
            Time.timeScale = 1;
            OnPauseStatusChange?.Invoke(Paused);
        }

        public void GameOver()
        {
            Paused = true;
            Time.timeScale = 0;
            OnPauseStatusChange?.Invoke(Paused);
            OnGameOver?.Invoke();
        }

        public void PauseResume()
        {
            if (!CanPause)
                return;

            if (Paused)
                ResumeGame();
            else
                PauseGame();
        }

        public void GameWin()
        {
            OnGameWin?.Invoke();
            FinishSaveStatusGame();
        }

        public void SetDeathCount()
        {
            deathCount++;
        }

        public void SetCollectableDreams()
        {
            collectableDreams++;
        }

        #region CODIGOS RANKING

        public void SetPlayerName(string nameUser)
        {
            playerDataRanking.playerName = nameUser;
        }

        public void StartTimeGame()
        {
            chrono.Start();
        }

        /// <summary>
        /// Função para indicar vitória da partida e salvar informações para o ranking
        /// </summary>
        public void FinishSaveStatusGame()
        {
            chrono.Stop();
            playerDataRanking.time = chrono.GetFinalTime();
            playerDataRanking.collectibleCount = collectableDreams;
            playerDataRanking.deathCount = deathCount;
            chrono.ResetTimer();
            
        }

        public void RankingClear()
        {
            playerDataRanking = new PlayerDataRanking();
            chrono = new Chrono();

            collectableDreams = 0;
            deathCount = 0;
        }

        #endregion

    }
}