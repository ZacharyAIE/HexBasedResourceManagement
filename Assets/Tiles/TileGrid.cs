using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public const int m_xAmount = 40;
    public const int m_yAmount = 40;
    public float xOffset = 1;
    public float yOffset = 0.866f;
    private float hexWidth = 1;
    private float hexHeight = 1.15f;

    public GameObject m_tilePrefab;

    private void Start()
    {

        GenerateGrid();
    }

    public void GenerateGrid()
    {
        for(int x=0; x<m_xAmount; x++)
        {
            for(int y =0; y<m_yAmount; y++)
            {
                GameObject go = Instantiate(m_tilePrefab);
                Vector2 gridPos = new Vector2(x, y);
                go.transform.position = CalcPosition(gridPos);
                go.transform.parent = this.transform;
                go.name = "Tile" + x + "|" + y;
            }
        }
    }

    public Vector3 CalcPosition(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexHeight / 2;

        float x = transform.position.x + gridPos.x * hexHeight + xOffset;
        float z = transform.position.z + gridPos.y * hexWidth * 0.75f;
        return new Vector3(x, 0, z);
    }
}
