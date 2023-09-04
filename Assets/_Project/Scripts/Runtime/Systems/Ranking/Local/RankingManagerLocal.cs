using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DreamTeam.Runtime.System.Ranking
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

        public TMPro.TextMeshProUGUI[] rPlayerName;
        public TMPro.TextMeshProUGUI[] rCollectibleCount;
        public TMPro.TextMeshProUGUI[] rDeathCount;
        public TMPro.TextMeshProUGUI[] rTime;


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
            SaveRanking();
        }

        void SaveRanking()
        {
            // Converter a lista em JSON e salvar no PlayerPrefs
            string rankingData = JsonUtility.ToJson(rankingList);
            PlayerPrefs.SetString("RankingData", rankingData);
            PlayerPrefs.Save();
        }

        void LoadRanking()
        {
            // Carregar o ranking salvo do PlayerPrefs
            if (PlayerPrefs.HasKey("RankingData"))
            {
                string rankingData = PlayerPrefs.GetString("RankingData");
                rankingList = JsonUtility.FromJson<List<PlayerDataRanking>>(rankingData);
            }
            else
            {
                rankingList = new List<PlayerDataRanking>();
            }
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
                if(rankingList.Count > i)
                {
                    rPlayerName[i].text = rankingList[i].playerName.ToString();
                    rCollectibleCount[i].text = rankingList[i].collectibleCount.ToString();
                    rDeathCount[i].text = rankingList[i].deathCount.ToString();
                    rTime[i].text = rankingList[i].time.ToString();
                }
            }
        }
    }
}
