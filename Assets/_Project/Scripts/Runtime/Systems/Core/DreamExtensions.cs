using System.Collections.Generic;

namespace DreamTeam.Runtime.Systems.Core
{
    public static class DreamExtensions
    {
        public static List<T> Shuffle<T>(this List<T> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                T temp = _list[i];
                int randomIndex = UnityEngine.Random.Range(i, _list.Count);
                _list[i] = _list[randomIndex];
                _list[randomIndex] = temp;
            }

            return _list;
        }
    }
}