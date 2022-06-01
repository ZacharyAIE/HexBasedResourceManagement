using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement
{
    
    [CreateAssetMenu(menuName = "Tile Type")]
    public class TileType : ScriptableObject
    {
        public string name;
        public int pathCost;
        public GameObject model;
        public Building defaultBuilding;
    }
}
