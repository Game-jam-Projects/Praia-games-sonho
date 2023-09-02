using DreamTeam.Runtime.System.Core;

namespace DreamTeam.Runtime.Systems.Core
{
    public class CoreSingleton : Singleton<CoreSingleton>
    {
        public GameStateManager gameStateManager;
    }
}
