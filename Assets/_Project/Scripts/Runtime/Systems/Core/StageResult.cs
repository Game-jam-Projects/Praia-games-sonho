using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DreamTeam.Runtime.Utilities;
using DreamTeam.Runtime.Systems.Ranking;
using UnityEngine.UI;

namespace DreamTeam.Runtime.Systems.Core
{
    public class StageResult : MonoBehaviour
    {

        public TMP_Text sonhos;
        public TMP_Text mortes;
        public TMP_Text tempo;
        public Button btnFinish;
        public TMP_InputField inputName;
        PlayerDataRanking playerDataRanking = new();

        // Start is called before the first frame update
        void Start()
        {

            sonhos.text = "Sonhos coletados, " + CoreSingleton.Instance.collectiblesManager.collected.Count.ToString() + " de " + CoreSingleton.Instance.collectiblesManager.totalCollectableDream.ToString();
            mortes.text = "Quantidade de mortes: " + CoreSingleton.Instance.gameManager.GetDeathCount().ToString();
            tempo.text = "Tempo Jogado: " + ConvertToHoursMinutesSeconds(Mathf.RoundToInt(CoreSingleton.Instance.gameManager.chrono.GetElapsedTime()));

            btnFinish.interactable = false;

            playerDataRanking.collectibleCount = CoreSingleton.Instance.collectiblesManager.collected.Count;
            playerDataRanking.deathCount = CoreSingleton.Instance.gameManager.GetDeathCount();
            playerDataRanking.time = CoreSingleton.Instance.gameManager.chrono.GetElapsedTime();

            inputName.onValueChanged.AddListener(RegisterName);
            btnFinish.onClick.AddListener(NextScene);

            //StartCoroutine(nameof(IEGoCut));
        }

        public static string ConvertToHoursMinutesSeconds(int totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }

        private void Update()
        {
//#if UNITY_EDITOR
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                StopCoroutine(nameof(IEGoCut));
//                SceneLoader.Instance.NextScene();
//            }
//#endif
        }

        private void NextScene()
        {
            StartCoroutine(nameof(GoCut));
        }

        IEnumerator IEGoCut()
        {
            yield return new WaitForSeconds(10);
            SceneLoader.Instance.NextScene();
        }

        private IEnumerator GoCut()
        {
            DreamUtilites.SaveRanking(playerDataRanking, "RankingDataGlobal");
            yield return new WaitForEndOfFrame();
            SceneLoader.Instance.NextScene();
        }

        public void RegisterName(string nameUser)
        {
            playerDataRanking.playerName = nameUser;
            if (string.IsNullOrEmpty(nameUser))
            {
                btnFinish.interactable = false;
            }
            else
            {
                btnFinish.interactable = true;
            }
        }
    }
}