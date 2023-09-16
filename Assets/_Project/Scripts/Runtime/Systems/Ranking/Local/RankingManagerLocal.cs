using DreamTeam.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Ranking
{
    [Serializable]
    public class PlayerDataRanking
    {
        public string playerName;
        public int collectibleCount;
        public int deathCount;
        public float time;
    }

    public class RankingManagerLocal : MonoBehaviour
    {

        public List<PlayerDataRanking> rankingList;

        public TextMeshProUGUI[] rPlayerName;
        public TextMeshProUGUI[] rCollectibleCount;
        public TextMeshProUGUI[] rDeathCount;
        public TextMeshProUGUI[] rTime;


        void Start()
        {
            // Verifique se existem dados do jogador salvos no DataManager.
            //if (DataManager.Instance.playerData != null)
            //{
            //    // Adicione os dados do jogador ao ranking.
            //    AddToRanking(DataManager.Instance.playerData);

            //    // Limpe os dados do jogador após adicioná-los ao ranking, se necessário.
            //    DataManager.Instance.playerData = null;
            //}

            // Carregar o ranking salvo (se existir)
            LoadRanking();
        }

        public void AddToRanking(PlayerDataRanking newPlayerData)
        {
            rankingList.Add(newPlayerData);

            // Ordenar a lista com base nas regras de classificação
            rankingList = rankingList.OrderBy(player => player.time)
                                     .ThenBy(player => player.deathCount)
                                     .ThenBy(player => player.collectibleCount)
                                     .ThenBy(player => player.playerName)
                                     .ToList();

            // Limitar a lista às 25 melhores entradas
            if (rankingList.Count > 25)
            {
                rankingList.RemoveAt(25);
            }

            // Salvar o ranking atualizado
            DreamUtilites.SaveRanking(rankingList, "RankingDataLocal");
        }

        void LoadRanking()
        {
            rankingList = DreamUtilites.LoadRankingList("RankingDataLocal");
            LoadingUI();
        }

        void LoadingUI()
        {
            for (int i = 0; i < rPlayerName.Count(); i++)
            {
                rPlayerName[i].text = " ---------- ";
                rCollectibleCount[i].text = "0";
                rDeathCount[i].text = "0";
                rTime[i].text = "0";
                if (rankingList.Count > i)
                {
                    rPlayerName[i].text = rankingList[i].playerName.ToString();
                    rCollectibleCount[i].text = rankingList[i].collectibleCount.ToString();
                    rDeathCount[i].text = rankingList[i].deathCount.ToString();
                    int minutos = Mathf.FloorToInt(rankingList[i].time / 60);
                    int segundos = Mathf.FloorToInt(rankingList[i].time % 60);
                    string textoFormatado = string.Format("{0:00}:{1:00}", minutos, segundos);
                    rTime[i].text = rankingList[i].time.ToString();
                }
            }
        }
    }
}
