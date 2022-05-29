using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace ResourceManagement
{
    public class ResourceCounterUI : MonoBehaviour
    {
        public List<TMP_Text> m_textBoxes = new List<TMP_Text>();
        public ResourceManager m_resourceManager;
        

        private void Awake()
        {
            foreach(TMP_Text t in GetComponentsInChildren<TMP_Text>())
            {
                m_textBoxes.Add(t);
            }

            m_resourceManager = FindObjectOfType<ResourceManager>();
        }

        [ContextMenu("Update")]
        public void UpdateUI()
        {
            for(int i = 0; i < m_textBoxes.Count; i++)
            {
                if (m_textBoxes[i].text != m_resourceManager.resourceList[i].ToString())
                {
                    m_textBoxes[i].transform.parent.transform.DOShakePosition(.5f, 20);
                    m_textBoxes[i].text = m_resourceManager.resourceList[i].ToString();
                }
            }
        }
    }
}
