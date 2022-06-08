using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace ResourceManagement.Tiles
{

    public class TileGrid : MonoBehaviour
    {
        [Tooltip("The number of tiles along the X Axis")]
        [SerializeField]public const int m_GridXSize = 30;
        [Tooltip("The number of tiles along the Y Axis")]
        [SerializeField]public const int m_GridYSize = 30;
        private float hexHeight = 1;
        private float hexWidth = 1;
        [Tooltip("The time a tile takes to reach its rest position")]
        public float animationLength = 0.5f;
        public float nextTileTime = 0.00004f;

        [Tooltip("The list of tiles you want to take from")]
        public TileList TileList;
        [Tooltip("Prefab for the tile")]
        public GameObject m_tilePrefab;
        [Tooltip("Enables Height Variation in generation")]
        public bool enableHeightVariation = false;

        private float verticalHexOffset = 0.866f;
        private float horizontalHexOffset = 0.5f;

        public List<GameObject> m_tiles = new List<GameObject>();
        public UnityEvent OnAnimationComplete;

        private void Awake()
        {
            TileList.InitTileList();
            GenerateGridNoVisuals();
        }

        private void Start()
        {
            AnimateGrid();
        }

        void AnimateGrid()
        {
            StartCoroutine(AnimateCo());
        }

        public void GenerateGridNoVisuals()
        {
            for (int x = 0; x < m_GridXSize; x++)
            {
                for (int y = 0; y < m_GridYSize; y++)
                {
                    var seed = Random.value;


                    // Select Tile type
                    GameObject go = Instantiate(m_tilePrefab);
                    Tile tile;
                    if (go.GetComponent<Tile>())
                    {
                        tile = go.GetComponent<Tile>();

                        if (x == 0 || x == m_GridXSize - 1)
                        {
                            tile.m_tileType = TileList.tileDictionary[TileTypes.Sand];
                        }
                        else if (y == 0 || y == m_GridYSize - 1)
                        {
                            tile.m_tileType = TileList.tileDictionary[TileTypes.Sand];
                        }
                        else
                        {
                            if (seed > 0.8)
                            {
                                tile.m_tileType = TileList.tileDictionary[TileTypes.Forest];
                            }
                            else if (seed < 0.8 && seed > 0.6)
                            {
                                tile.m_tileType = TileList.tileDictionary[TileTypes.Swamp];
                            }
                            else if (seed < 0.1)
                            {
                                tile.m_tileType = TileList.tileDictionary[TileTypes.Stone];
                            }
                            else
                            {
                                tile.m_tileType = TileList.tileDictionary[TileTypes.Grass];
                            }
                        }
                    }   

                    if (go)
                    {
                        Vector2 gridPos = new Vector2(x, y);
                        go.transform.position = CalcPosition(gridPos);
                        go.transform.parent = this.transform;
                        go.name = go.GetComponent<Tile>().m_tileType.tileName + x + "|" + y;
                        m_tiles.Add(go);
                        go.gameObject.SetActive(false);
                    }
                    
                }
            }
        }

        public Vector3 CalcPosition(Vector2 gridPos)
        {
            float offset = 0;
            if (gridPos.y % 2 != 0)
            {
                offset = hexHeight / 2;
            }


            float x = transform.position.x + gridPos.x * hexWidth + horizontalHexOffset;
            if (gridPos.y % 2 != 0)
            {
                x += .5f;
            }
            float z = transform.position.z + gridPos.y * hexHeight * verticalHexOffset;
            return new Vector3(x, 5, z);
        }

        public IEnumerator AnimateCo()
        {
            

            foreach (GameObject tile in m_tiles)
            {
                var seed = Random.value;
                if (tile != null)
                {
                    tile.SetActive(true);
                    if(seed < 0.3 && tile.GetComponent<Tile>().m_tileType != TileList.tileDictionary[TileTypes.Sand] && enableHeightVariation)
                    {
                        tile.transform.DOMove(new Vector3(tile.transform.position.x, tile.transform.position.y - Random.Range(4.8f, 4.9f), tile.transform.position.z), animationLength);
                    }
                    else 
                    {
                        tile.transform.DOMove(new Vector3(tile.transform.position.x, tile.transform.position.y - 5, tile.transform.position.z), animationLength);
                    }

                    yield return new WaitForSeconds(nextTileTime);
                }
            }
            OnAnimationComplete.Invoke();
        }

        public void RegenGrid()
        {
            ClearGrid();
            GenerateGridNoVisuals();
            StartCoroutine(AnimateCo());
            
        }

        public void ClearGrid()
        {
            if(m_tiles.Count > 0)
            {
                for (int i = 0; i < m_tiles.Count; i++)
                {
                    Destroy(m_tiles[i]);
                    m_tiles[i] = null;
                }
            }
        }
    }
}