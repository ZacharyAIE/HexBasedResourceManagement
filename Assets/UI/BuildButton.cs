using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceManagement.BuildingSystem
{
    /// <summary>
    /// This class is purely to serve the tooltip data about the the building it creates.
    /// </summary>
    public class BuildButton : MonoBehaviour
    {
        [HideInInspector]public Building buildingData;
        public Image buttonImageIcon;
        public Button button;
        public void SetBuilding(Building building, BuildCursor buildCursor)
        {
            buildingData = building;
            button = GetComponent<Button>();
            buttonImageIcon.sprite = building.buttonSprite;
            button.onClick.AddListener(() => { buildCursor.ClearCursor(true); buildCursor.buildingToBuy = building; });
        }
    }
}


