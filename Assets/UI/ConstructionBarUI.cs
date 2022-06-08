using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ResourceManagement.BuildingSystem;


public class ConstructionBarUI : MonoBehaviour
{
    public BuildingList buildings;
    public BuildButton buttonPrefab;
    public BuildCursor buildCursor;

    private void Awake()
    {
        buildCursor = FindObjectOfType<BuildCursor>();
        PopulateUI();
    }

    public void PopulateUI()
    {
        foreach (Building b in buildings.list)
        {
            if (b)
            {
                var button = Instantiate(buttonPrefab, this.gameObject.transform).GetComponent<BuildButton>();
                button.SetBuilding(b, buildCursor);
            }
        }
    }
}

