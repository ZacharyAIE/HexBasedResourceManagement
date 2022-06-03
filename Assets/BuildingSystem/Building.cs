using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement.BuildingSystem
{

    [CreateAssetMenu(menuName = "Create Building")]
    public class Building : ScriptableObject
    {
        public string name;
        public Sprite buttonSprite;
        public string description;

        public int woodCost;
        public int goldCost;
        public int stoneCost;

        public GameObject model;

    }
}