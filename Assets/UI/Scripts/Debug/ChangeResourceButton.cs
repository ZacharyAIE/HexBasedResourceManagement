using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement.Debugging
{

    public class ChangeResourceButton : MonoBehaviour
    {
        ResourceManager resourceManager;
        public ResourceType resourceType;
        public int amountToAdd;
        // Start is called before the first frame update
        void Start()
        {
            resourceManager = FindObjectOfType<ResourceManager>();
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { resourceManager.SetResource(resourceType, amountToAdd); });
            //resourceManager.resourceCounter.UpdateUI();
        }
    }
}