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
        public UnityEvent<ResourceType> resourcesUpdated;

        void Awake()
        {
            InitResources();
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
            resourcesUpdated.Invoke(rt);
        }

        void InitResources()
        {
            foreach(ResourceType rt in System.Enum.GetValues(typeof(ResourceType)))
                if(rt != ResourceType.None)
                    resources[rt] = 200;

            resources[ResourceType.People] = startingPeople;
        }
    }
}
