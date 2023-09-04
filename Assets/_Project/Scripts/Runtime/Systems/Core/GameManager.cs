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
        public event Action OnTransitionFinished;
        public event Action<int> OnUpdateSwapItemAmount;

        public bool Paused { get; private set; }


        [Header("Ranking")]
        [SerializeField] private int collectableDreams;
        [SerializeField] private int deathCount;
        public PlayerDataRanking playerDataRanking;
        public Chrono chrono;

        [Header("Seletor de Personagem")]
        [SerializeField] private int idChar;
        public Character[] characters;

        public int swapDreamItem;

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

        public void ForcedPauseGame()
        {
            Paused = true;
            chrono.Stop();
            Time.timeScale = 0;

            if(CanPause)
                OnPauseStatusChange?.Invoke(Paused);
        }

        public void ForcedResumeGame()
        {
            Paused = false;
            chrono.Start();
            Time.timeScale = 1;

            if(CanPause)
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
                ForcedResumeGame();
            else
                ForcedPauseGame();
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

        public void SelectChar(int idChar)
        {
            this.idChar = idChar;
        }

        public Character GetCharacter()
        {
            return characters[this.idChar];
        }

        public void SetItem(int value)
        {
            swapDreamItem += value;

            if (swapDreamItem < 0)
                return;

            OnUpdateSwapItemAmount?.Invoke(swapDreamItem);
        }

        public int GetItem()
        {
            return swapDreamItem;
        }

        public void TriggerTransitionFinish()
        {
            OnTransitionFinished?.Invoke();
        }
    }

    
}

[Serializable]
public struct Character
{
    public string playerName;
    public RuntimeAnimatorController dreamController;
    public RuntimeAnimatorController nightmareController;
}