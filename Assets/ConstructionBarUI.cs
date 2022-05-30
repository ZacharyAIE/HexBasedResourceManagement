using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceManagement
{

    public class ConstructionBarUI : MonoBehaviour
    {
        public BuildingList buildings;
        public GameObject buttonPrefab;
        public BuildCursor buildCursor;

        private void Start()
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
                    var button = Instantiate(buttonPrefab, this.gameObject.transform);
                    button.GetComponentsInChildren<Image>()[1].sprite = b.buttonSprite;
                    button.GetComponentInChildren<Button>().onClick.AddListener(()=> buildCursor.buildingToBuy = b);
                }
            }
        }
    }
}

