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
    TileType m_tileType;

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
        base.OnSelect();
    }
}
