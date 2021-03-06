using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace ResourceManagement
{

    public class ResourceUI : MonoBehaviour
    {
        public ResourceType resourceType;
        public ResourceManager resourceManager;
        public TMP_Text textBox;
        public Image image;
        [Header("UI Shake Settings")]
        public float shakeDuration = 0.5f;
        public float shakeStrength = 30;
        public int shakeVibrato = 30;

        public void InitUI(ResourceType rt)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
            resourceType = rt;
            textBox.text = resourceManager.GetResource(resourceType).ToString();

            image.sprite = Resources.Load<Sprite>(rt.ToString());
        }

        public void UpdateUI()
        {
            var temp = resourceManager.GetResource(resourceType).ToString();
            if (temp != textBox.text)
            {
                transform.DORestart();
                transform.DOKill();
                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, 20, false, true);
                textBox.text = resourceManager.GetResource(resourceType).ToString();
            }
        }
    }
}