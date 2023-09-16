using DreamTeam.Runtime.Systems.Ranking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public static class DreamUtilites
    {
        public static string ConvertToHoursMinutesSeconds(float totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }

        public static void SaveRanking(List<PlayerDataRanking> dataRanking, string nameSave)
        {
            // Converter a lista em JSON e salvar no PlayerPrefs
            string rankingData = JsonUtility.ToJson(dataRanking);
            PlayerPrefs.SetString(nameSave, rankingData);
            PlayerPrefs.Save();
        }

        public static void SaveRanking(PlayerDataRanking dataRanking, string nameSave)
        {
            // Converter a lista em JSON e salvar no PlayerPrefs
            string rankingData = JsonUtility.ToJson(dataRanking);
            PlayerPrefs.SetString(nameSave, rankingData);
            PlayerPrefs.Save();
        }

        public static List<PlayerDataRanking> LoadRankingList(string nameLoad)
        {
            List<PlayerDataRanking> rankingList = new();

            // Carregar o ranking salvo do PlayerPrefs
            if (PlayerPrefs.HasKey(nameLoad))
            {
                string rankingData = PlayerPrefs.GetString(nameLoad);
                return JsonUtility.FromJson<List<PlayerDataRanking>>(rankingData);
            }
            else
            {
                return rankingList;
            }
        }

        public static PlayerDataRanking LoadRanking(string nameLoad)
        {
            PlayerDataRanking rankingList = new();

            // Carregar o ranking salvo do PlayerPrefs
            if (PlayerPrefs.HasKey(nameLoad))
            {
                string rankingData = PlayerPrefs.GetString(nameLoad);
                return JsonUtility.FromJson<PlayerDataRanking>(rankingData);
            }
            else
            {
                return rankingList;
            }
        }
    }
}
