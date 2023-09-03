using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class DreamNightmareItemManager : MonoBehaviour
{
    public GameObject dream;
    public GameObject nigthmare;
     // Start is called before the first frame update
    void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
    }

    private void OnDisable()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;
    }
    public void EChangeStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:
                dream.SetActive(true);
                nigthmare.SetActive(false);
                break;

            case StageType.Nightmare:
                dream.SetActive(false);
                nigthmare.SetActive(true);
                break;
        }
    }
}
