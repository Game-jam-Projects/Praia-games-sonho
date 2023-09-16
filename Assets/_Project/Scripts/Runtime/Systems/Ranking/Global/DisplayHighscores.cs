using DreamTeam.Runtime.Utilities;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Ranking
{
    public class DisplayHighscores : MonoBehaviour
    {
        public TextMeshProUGUI[] rNames;
        public TextMeshProUGUI[] rCollectables;
        public TextMeshProUGUI[] rDeath;
        public TextMeshProUGUI[] rTime;

        HighScores myScores;

        void Start() //Fetches the Data at the beginning
        {
            for (int i = 0; i < rNames.Length; i++)
            {
                rNames[i].text = i + 1 + ". Fetching...";
            }

            myScores = GetComponent<HighScores>();
            SetRanking();
        }
        public void SetScoresToMenu(UIPlayerScore[] highscoreList) //Assigns proper name and score for each text value
        {
            highscoreList = OrganizeRanking(highscoreList);
            for (int i = 0; i < rNames.Length; i++)
            {
                rNames[i].text = "-----";
                rCollectables[i].text = "0";
                rDeath[i].text = "0";
                rTime[i].text = "0";

                if (highscoreList.Length > i)
                {
                    rNames[i].text = highscoreList[i].username;
                    rCollectables[i].text = highscoreList[i].collectableItem.ToString();
                    rDeath[i].text = highscoreList[i].deathCount.ToString();
                    rTime[i].text = highscoreList[i].time;
                }
            }
        }

        private UIPlayerScore[] OrganizeRanking(UIPlayerScore[] highScoreList)
        {
           return highScoreList
            .OrderBy(player => player.time) // Classificar pelo tempo (menor tempo primeiro).
            .ThenBy(player => player.deathCount) // Em caso de empate, classificar pela contagem de mortes (menor mortes primeiro).
            .ThenBy(player => player.collectableItem) // Em caso de empate, classificar pela quantidade de itens coletados (mais itens primeiro).
            .ThenBy(player => player.username) // Em caso de empate, classificar alfabeticamente pelo nome de usuário.
            .ToArray();
        }

        private void SetRanking()
        {
            if (PlayerPrefs.HasKey("RankingDataGlobal"))
            {
                PlayerDataRanking playerDataRanking = DreamUtilites.LoadRanking("RankingDataGlobal");
                myScores.UploadScore(playerDataRanking.playerName, playerDataRanking.collectibleCount, playerDataRanking.deathCount, playerDataRanking.time);
                //PlayerPrefs.DeleteKey("RankingDataGlobal");
                StartCoroutine("RefreshHighscores");
            }
            else
            {
                StartCoroutine("RefreshHighscores");
            }
        }

        IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
        {
            while (true)
            {
                yield return new WaitForSeconds(10);
                myScores.DownloadScores();
            }
        }
    }
}