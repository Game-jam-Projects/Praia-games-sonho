﻿using System.Collections;
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
            StartCoroutine("RefreshHighscores");
        }
        public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
        {
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
                    int minutos = Mathf.FloorToInt(highscoreList[i].time / 60);
                    int segundos = Mathf.FloorToInt(highscoreList[i].time % 60);
                    string textoFormatado = string.Format("{0:00}:{1:00}", minutos, segundos);
                    rTime[i].text = textoFormatado;
                }
            }
        }
        IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
        {
            while (true)
            {
                myScores.DownloadScores();
                yield return new WaitForSeconds(30);
            }
        }
    }
}