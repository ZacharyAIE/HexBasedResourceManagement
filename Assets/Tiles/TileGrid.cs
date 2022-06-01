using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ResourceManagement
{

    public class TileGrid : MonoBehaviour
    {
        public const int m_xAmount = 20;
        public const int m_yAmount = 20;
        private float hexHeight = 1;
        private float hexWidth = 1;

        public TileList TileList;
        public GameObject m_tilePrefab;
        public AudioClip tilePlacedSound;

        public List<GameObject> m_tiles = new List<GameObject>();

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
            for (int x = 0; x < m_xAmount; x++)
            {
                for (int y = 0; y < m_yAmount; y++)
                {
                    var seed = Random.value;

                    GameObject go = Instantiate(m_tilePrefab);
                    if (x == 0 || x == m_xAmount - 1)
                    {
                        go.GetComponent<Tile>().m_tileType = TileList.tileDictionary[TileTypes.Sand];
                    }
                    else if (y == 0 || y == m_yAmount - 1)
                    {
                        go.GetComponent<Tile>().m_tileType = TileList.tileDictionary[TileTypes.Sand];
                    }
                    else
                    {
                        if (seed > 0.8)
                        {
                            go.GetComponent<Tile>().m_tileType = TileList.tileDictionary[TileTypes.Forest];
                        }
                        else if (seed < 0.1)
                        {
                            go.GetComponent<Tile>().m_tileType = TileList.tileDictionary[TileTypes.Stone];
                        }
                        else
                        {
                            go.GetComponent<Tile>().m_tileType = TileList.tileDictionary[TileTypes.Grass];
                        }
                    }

                    Vector2 gridPos = new Vector2(x, y);
                    go.transform.position = CalcPosition(gridPos);
                    go.transform.parent = this.transform;
                    go.name = go.GetComponent<Tile>().m_tileType.name + x + "|" + y;
                    m_tiles.Add(go);
                    go.gameObject.SetActive(false);
                    StartCoroutine(AnimateCo());
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


            float x = transform.position.x + gridPos.x * hexWidth + 0.5f;
            if (gridPos.y % 2 != 0)
            {
                x += .5f;
            }
            float z = transform.position.z + gridPos.y * hexHeight * 0.866f;
            return new Vector3(x, 5, z);
        }

        public IEnumerator AnimateCo()
        {
            foreach (GameObject tile in m_tiles)
            {
                if (tile != null)
                {
                    tile.SetActive(true);
                    tile.transform.DOMove(new Vector3(tile.transform.position.x, tile.transform.position.y - 5, tile.transform.position.z), 0.5f);
                    tile.GetComponent<AudioSource>().PlayOneShot(tilePlacedSound);
                    yield return new WaitForSeconds(0.004f);
                }
            }
        }

        public void RegenGrid()
        {
            ClearGrid();
            GenerateGridNoVisuals();
            StartCoroutine(AnimateCo());
        }

        public void ClearGrid()
        {
            for (int i = 0; i < m_tiles.Count; i++)
            {
                Destroy(m_tiles[i]);
                m_tiles[i] = null;
            }
        }
    }
}