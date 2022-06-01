using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement
{
    public enum TileTypes
    {
        Stone,
        Sand,
        Grass,
        Forest
    }

    [CreateAssetMenu(menuName = "Tile Type List")]
    public class TileList : ScriptableObject
    {
        public Dictionary<TileTypes, TileType> tileDictionary;
        public List<TileType> tilePrefabs;

        public void InitTileList()
        {
            System.Array enumArray = System.Enum.GetValues(typeof(TileTypes));

            tileDictionary = new Dictionary<TileTypes, TileType>();

            for(int i = 0 ; i < tilePrefabs.Count; i++)
            {
                tileDictionary.Add((TileTypes)enumArray.GetValue(i), tilePrefabs[i]);
            }
        }
    }
}
