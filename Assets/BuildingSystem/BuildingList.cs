using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement.BuildingSystem
{
    [CreateAssetMenu(menuName = "Building List")]
    public class BuildingList : ScriptableObject
    {
        public List<Building> list;
    }
}


