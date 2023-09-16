using UnityEngine;
namespace DreamTeam.Runtime.Systems.Core
{
    public class CoreSingleton : Singleton<CoreSingleton>
    {
        public GameStateManager gameStateManager;
        public GameManager gameManager;
        public CollectiblesManager collectiblesManager;

        [HideInInspector] public CamerasManager camerasManager;
        [HideInInspector] public PlayerController playerController;

    }
}
