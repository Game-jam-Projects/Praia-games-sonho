using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    const string privateCode = "ea8ZIlk150OUkRRYg68vNgpdQpWtycyk6ohYCQPuTBRw";  //Key to Upload New Info
    const string publicCode = "64f6391a8f40bb0ee070f37d";   //Key to download
    const string webURL = "http://dreamlo.com/lb/"; //  Website the keys are for

    public PlayerScore[] scoreList;
    DisplayHighscores myDisplay;

    static HighScores instance; //Required for STATIC usability
    void Awake()
    {
        instance = this; //Sets Static Instance
        myDisplay = GetComponent<DisplayHighscores>();

        float a = 92.45f;
        float b = 100.13f;
        UploadScore("Pedrin", 42, 27, a);
        UploadScore("pedrilho", 35, 30, b);
    }
    
    public static void UploadScore(string username, int collectableItem, int deathCount, float time)  //CALLED when Uploading new Score to WEBSITE
    {//STATIC to call from other scripts easily
        instance.StartCoroutine(instance.DatabaseUpload(username,collectableItem, deathCount, time)); //Calls Instance
    }

    IEnumerator DatabaseUpload(string userame, int collectableItem, int deathCount, float time) //Called when sending new score to Website
    {
        string timeString = time.ToString();
        print("TIIIME : " + timeString);
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(userame) + "/" + collectableItem + "/" + deathCount + "/" + timeString);
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
        //WWW www = new WWW(webURL + publicCode + "/pipe/"); //Gets the whole list
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
        string[] entries = rawData.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        scoreList = new PlayerScore[entries.Length];
        for (int i = 0; i < entries.Length; i ++) //For each entry in the string array
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int collectableItem = int.Parse(entryInfo[1]);
            int deathCount = int.Parse(entryInfo[2]);
            string stringTeste = "42,60";
            float time = float.Parse(stringTeste);
            string timeString = entryInfo[3].Trim();
            CultureInfo culture = new CultureInfo("fr-FR");
            //timeString = timeString.Replace(',', '.');
            print($"teste: {stringTeste} / time: {timeString}");
            print($"CONVERT TESTE: {float.Parse(stringTeste,culture)}");
            print($"TIME: {float.Parse(timeString,culture)}");

            scoreList[i] = new PlayerScore(username,collectableItem, deathCount, time);
            print(scoreList[i].username + ": " + scoreList[i].collectableItem);
        }
    }
}

public struct PlayerScore //Creates place to store the variables for the name and score of each player
{
    public string username;
    public int collectableItem;
    public int deathCount;
    public float time;

    public PlayerScore(string _username, int _collectableItem, int _deathCount, float _time)
    {
        username = _username;
        collectableItem = _collectableItem;
        deathCount = _deathCount;
        time = _time;
    }
}