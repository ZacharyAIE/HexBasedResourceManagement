using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ResourceManagement.BuildingSystem
{

    [CreateAssetMenu(menuName = "Create Building")]
    public class Building : ScriptableObject
    {
        public string buildingName;
        public Sprite buttonSprite;
        public string description;

        Dictionary<ResourceType, int> _costs;
        Dictionary<ResourceType, int> costs
        {
            get 
            { 
                if(_costs == null)
                {
                    _costs = new Dictionary<ResourceType,int>();
                    foreach(Cost cost in costList)
                    {
                        _costs[cost.resourceType]= cost.resourceAmount;
                    }
                }
                return _costs; 
            }
        }

        public List<Cost> costList;

        public GameObject model;

        public int GetCost(ResourceType rt)
        {
            if(!costs.ContainsKey(rt))
                return 0;

            return costs[rt];
        }

    }

    [System.Serializable]
    public struct Cost
    {
        public ResourceType resourceType;
        public int resourceAmount;

    }
}