using UnityEngine;

namespace DreamTeam.Runtime.Utilities.ChronoTimer
{
    [System.Serializable]
    public class Chrono
    {
        private float startTime;
        private float stopTime;
        private float pausedTime;
        private bool isRunning = false;

        /// <summary>
        /// Inicia o cron�metro.
        /// </summary>
        public void Start()
        {
            if (!isRunning)
            {
                startTime = Time.time - pausedTime;
                isRunning = true;
            }
        }

        /// <summary>
        /// Para o cron�metro, registrando o tempo decorrido at� o momento.
        /// </summary>
        public void Stop()
        {
            if (isRunning)
            {
                stopTime = Time.time;
                isRunning = false;
                pausedTime = stopTime - startTime;
            }
        }

        /// <summary>
        /// Obt�m o tempo decorrido at� o momento (incluindo o tempo durante a pausa).
        /// </summary>
        /// <returns>O tempo decorrido.</returns>
        public float GetElapsedTime()
        {
            if (isRunning)
            {
                return Time.time - startTime;
            }
            else
            {
                return pausedTime;
            }
        }

        /// <summary>
        /// Obt�m o tempo total decorrido desde o in�cio at� a parada definitiva.
        /// </summary>
        /// <returns>O tempo total decorrido.</returns>
        public float GetFinalTime()
        {
            if (isRunning)
            {
                return stopTime - startTime;
            }
            else
            {
                return pausedTime;
            }
        }

        /// <summary>
        /// Resetar o contador
        /// </summary>
        public void ResetTimer()
        {
            startTime = 0;
            stopTime = 0;
            pausedTime = 0;
            isRunning = false;
        }
    }
}


