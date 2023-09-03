using DreamTeam.Runtime.System.Core;
using UnityEngine;
namespace DreamTeam.Runtime.Systems.Core
{
    public class CoreSingleton : Singleton<CoreSingleton>
    {
        public GameStateManager gameStateManager;

       [HideInInspector] public PlayerController playerController;
        
    }
}
