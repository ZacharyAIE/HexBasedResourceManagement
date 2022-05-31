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
        public TMP_Text messageBox;
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
            foreach (ResourceManager.ResourceType rt in System.Enum.GetValues(typeof(ResourceManager.ResourceType)))
            {
                ResourceUI ui = Instantiate(uiBoxPrefab, this.transform);
                ui.gameObject.name = rt.ToString() + " Resource UI";
                resourceUIElements.Add(ui);
                ui.InitUI(rt);
            }
        }
        ///FIX THIS STUFF!!!!!!!!!!!!!!
        ///!!!!!!!!!!!!!!!!!!!!!!!
        ///!!!!!!!!!!!!!
        public void BuildFailed()
        {
            EnableFailText();
            if (messageBox.enabled != false)
                Invoke("DisableFailText", 2f);
        }

        void EnableFailText()
        {
            if (messageBox)
            {
                messageBox.enabled = true;
            }
        }
        void DisableFailText()
        {
            if (messageBox)
            {
                messageBox.enabled = false;
            }
        }
    }
}
