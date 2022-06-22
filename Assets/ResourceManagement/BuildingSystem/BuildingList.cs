using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement.BuildingSystem
{
    [CreateAssetMenu(menuName = "Building List")]
    public class BuildingList : ScriptableObject
    {
        [Tooltip("After creating a building Scriptable Object, place it here to make it buildable by the player.")]
        public List<Building> list;
    }
}


