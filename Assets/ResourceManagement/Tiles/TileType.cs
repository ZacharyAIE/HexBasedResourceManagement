using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceManagement.BuildingSystem;

namespace ResourceManagement.Tiles
{
    
    [CreateAssetMenu(menuName = "Tile Type")]
    public class TileType : ScriptableObject
    {
        public string tileName;
        public int pathCost;
        public GameObject model;
        public Building defaultBuilding;
    }
}
