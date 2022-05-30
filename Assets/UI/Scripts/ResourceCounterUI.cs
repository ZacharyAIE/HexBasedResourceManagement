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
        public List<TMP_Text> m_textBoxes = new List<TMP_Text>();
        public ResourceManager m_resourceManager;
        public UnityEvent updateEvent;
        public TMP_Text messageBox;

        private void Awake()
        {
            foreach(TMP_Text t in GetComponentsInChildren<TMP_Text>())
            {
                m_textBoxes.Add(t);
            }

            m_resourceManager = FindObjectOfType<ResourceManager>();

            updateEvent.AddListener(()=> UpdateUI());
        }

        [ContextMenu("Update")]
        public void UpdateUI()
        {
            for(int i = 0; i < m_textBoxes.Count; i++)
            {
                if (m_textBoxes[i].text != m_resourceManager.resourceList[i].ToString())
                {
                    DOTween.CompleteAll();
                    Vector3 temp = m_textBoxes[i].transform.parent.position;
                    m_textBoxes[i].transform.parent.position = temp;
                    m_textBoxes[i].transform.parent.transform.DOShakePosition(.5f, 10);
                    m_textBoxes[i].text = m_resourceManager.resourceList[i].ToString();
                }
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
