using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ResourceManagement
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] public int startingPeople = 2;

        public ResourceCounterUI resourceCounter;
        public UnityEvent resourcesUpdated;

        void Awake()
        {
            InitResources();
        }

        public enum ResourceType
        {
            Wood,
            Stone,
            Gold,
            People
        }

        public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

        public int GetResource(ResourceType rt)
        {
            if (resources.ContainsKey(rt))
                return resources[rt];
            return 0;
        }

        public void SetResource(ResourceType rt, int amount)
        {
            resources[rt] += amount;
            resourcesUpdated.Invoke();
        }

        void InitResources()
        {
            foreach(ResourceType rt in System.Enum.GetValues(typeof(ResourceType)))
                resources[rt] = 200;

            resources[ResourceType.People] = startingPeople;
        }
    }
}
