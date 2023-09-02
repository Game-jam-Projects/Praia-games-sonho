using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace patrick
{
    public class Core : MonoBehaviour
    {
        public static Core Instance;

        public GameManager gameManager;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }
    }
}
