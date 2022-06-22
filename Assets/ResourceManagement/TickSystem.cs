using System;
using UnityEngine;
using UnityEngine.Events;

namespace ResourceManagement
{
    public class TickSystem : MonoBehaviour
    {
        // Singleton instance of the TickSystem
        public static TickSystem Instance { get; private set; }

        [SerializeField] private float tickFrequency = 1;
        private static int currentTick = 0;
        public static int CurrentTick => currentTick;
        private float lastTickTime = 0;
        [SerializeField] private static float currentGameTime;
        public static float CurrentGameTime => currentGameTime;

        public UnityEvent OnTick;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void Update()
        {
            Tick();
        }

        void Tick()
        {
            // Add a tick and make sure it ticks based on the tick frequency
            currentGameTime += Time.deltaTime;

            if (currentGameTime >= lastTickTime + tickFrequency)
            {
                lastTickTime = currentGameTime;
                OnTick.Invoke();
                currentTick++;
            }
        }
    }
}