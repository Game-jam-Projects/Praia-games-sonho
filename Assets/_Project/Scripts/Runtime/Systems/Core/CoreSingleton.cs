using DreamTeam.Runtime.System.Core;
using UnityEngine;
namespace DreamTeam.Runtime.Systems.Core
{
    public class CoreSingleton : Singleton<CoreSingleton>
    {
        public GameStateManager gameStateManager;
        public GameManager gameManager;

        [HideInInspector] public CamerasManager camerasManager;
        [HideInInspector] public PlayerController playerController;

    }
}
