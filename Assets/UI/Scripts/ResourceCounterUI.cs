using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

namespace ResourceManagement
{
    public class ResourceCounterUI : MonoBehaviour
    {
        public List<ResourceUI> resourceUIElements = new List<ResourceUI>();
        public ResourceManager m_resourceManager;
        public GameObject messageBox;
        public ResourceUI uiBoxPrefab;

        private void Start()
        {
            CreateResourceUI();
        }

        [ContextMenu("Update")]
        public void UpdateUI()
        {
            foreach (ResourceUI ui in resourceUIElements)
            {
                ui.UpdateUI();
            }
        }

        public void CreateResourceUI()
        {
            foreach (ResourceType rt in System.Enum.GetValues(typeof(ResourceType)))
            {
                if(rt != ResourceType.None)
                {
                    ResourceUI ui = Instantiate(uiBoxPrefab, this.transform);
                    ui.gameObject.name = rt.ToString() + " Resource UI";
                    resourceUIElements.Add(ui);
                    ui.InitUI(rt);
                }
            }
        }

        public void BuildFailed()
        {
            EnableFailText();
            if (messageBox.activeSelf != false)
                Invoke("DisableFailText", 2f);
        }

        void EnableFailText()
        {
            if (messageBox)
            {
                messageBox.SetActive(true);
            }
        }
        void DisableFailText()
        {
            if (messageBox)
            {
                messageBox.SetActive(false);
            }
        }
    }
}
