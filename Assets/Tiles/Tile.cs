using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    GRASS,
    TOWN,
    FARM,
    FOREST,
    DESERT
}

public class Tile : Clickable
{
    public ResourceManagement.ResourceManager resourceManager;
    BuildingManager buildingManager;
    TileType m_tileType;
    public Building m_building;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
    }

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
    }

    public override void OnSelect()
    {
        if (!m_building )
        {
            if (resourceManager && resourceManager.GoldAmount >= m_building.cost)
            {
                m_building = buildingManager.buildingToBuy;
                resourceManager.AdjustGoldCount(m_building.cost);
                Instantiate(m_building.model, m_snapPoint.position, Quaternion.identity);
            }

            else
            {
                Debug.Log("Insufficient Gold");
            }
        }
    }
}
