using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Utilities;
using System.Collections;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Ranking
{
    public class HighScoreHandler : Singleton<HighScoreHandler>
    {
        const string privateCode = "ea8ZIlk150OUkRRYg68vNgpdQpWtycyk6ohYCQPuTBRw";  //Key to Upload New Info
        const string publicCode = "64f6391a8f40bb0ee070f37d";   //Key to download
        const string webURL = "http://dreamlo.com/lb/"; //  Website the keys are for

        public UIPlayerScore[] scoreList;
        DisplayHighscores myDisplay;

        static HighScoreHandler instance; //Required for STATIC usability
        protected override void Awake()
        {
            base.Awake();

            instance = this; //Sets Static Instance
            myDisplay = GetComponent<DisplayHighscores>();
        }

        public void UploadScore(string username, int collectableItem, int deathCount, float time)
        {
            Debug.Log("Uploaded time: " + time);

            instance.StartCoroutine(instance.DatabaseUpload(username, collectableItem, deathCount, time));
        }

        private IEnumerator DatabaseUpload(string userame, int collectableItem, int deathCount, float time)
        {
            WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(userame) + "/" + collectableItem + "/" + deathCount + "/" + time.ToString());
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                print("Upload Successful");
                DownloadScores();
            }
            else print("Error uploading" + www.error);
        }

        public void DownloadScores()
        {
            StartCoroutine("DatabaseDownload");
        }
        IEnumerator DatabaseDownload()
        {
            WWW www = new WWW(webURL + publicCode + "/pipe"); //Gets top 10
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                OrganizeInfo(www.text);
                myDisplay.SetScoresToMenu(scoreList);
            }
            else print("Error uploading" + www.error);
        }

        void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
        {
            print(rawData);

            string[] entries = rawData.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            scoreList = new UIPlayerScore[entries.Length];
            for (int i = 0; i < entries.Length; i++) //For each entry in the string array
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];

                int collectableItem = int.Parse(entryInfo[1]);
                int deathCount = int.Parse(entryInfo[2]);

                float time = float.Parse(entryInfo[3]);

                string convertedTime = DreamUtilites.ConvertToHoursMinutesSeconds(time);

                scoreList[i] = new UIPlayerScore(username, collectableItem, deathCount, convertedTime);
                print(scoreList[i].username + ": " + scoreList[i].collectableItem + "Converted> " + convertedTime);
            }
        }
    }
}

namespace DreamTeam.Runtime.Systems.Ranking
{
    public struct UIPlayerScore //Creates place to store the variables for the name and score of each player
    {
        public string username;
        public int collectableItem;
        public int deathCount;
        public string time;

        public UIPlayerScore(string _username, int _collectableItem, int _deathCount, string _time)
        {
            username = _username;
            collectableItem = _collectableItem;
            deathCount = _deathCount;
            time = _time;
        }
    }
}