using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace patrick
{
    public enum StageType
    {
        Dream, Nightmare
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private StageType stageType;

        public delegate void ChangeType(StageType stageType);
        public event ChangeType ChagedStageType;

        
        public GameObject nightmarePostProcessing;

        public void ChangeStageType(StageType stageType)
        {
            this.stageType = stageType;
           
            if(ChagedStageType != null)
            {
                ChagedStageType(this.stageType);
            }
        }

        public StageType GetStageType()
        {
            return stageType;
        }

        public void ChangeStageType()
        {
            switch(stageType)
            {
                case StageType.Dream:
                    ChangeStageType(StageType.Nightmare);
                    break;

                case StageType.Nightmare:
                    ChangeStageType(StageType.Dream);
                    break;
            }
        }

    }
}
