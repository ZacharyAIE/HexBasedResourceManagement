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
        public ResourceManager.ResourceType resourceType;
        public ResourceManager resourceManager;
        public TMP_Text textBox;
        public Image image;
        [Header("UI Shake Settings")]
        public float shakeDuration = 0.5f;
        public float shakeStrength = 30;
        public int shakeVibrato = 30;

        public void InitUI(ResourceManager.ResourceType rt)
        {
            resourceManager = FindObjectOfType<ResourceManager>();
            resourceType = rt;
            textBox.text = resourceManager.GetResource(resourceType).ToString();

            switch (rt)
            {
                case ResourceManager.ResourceType.Stone:
                    image.sprite = Resources.Load<Sprite>("Stone");
                   break;
                case ResourceManager.ResourceType.Wood:
                    image.sprite = Resources.Load<Sprite>("Wood");
                    break;
                case ResourceManager.ResourceType.Gold:
                    image.sprite = Resources.Load<Sprite>("Coin");
                    break;
                case ResourceManager.ResourceType.People:
                    image.sprite = Resources.Load<Sprite>("People");
                    break;
            }
        }

        public void UpdateUI()
        {
            var temp = resourceManager.GetResource(resourceType).ToString();
            if (temp != textBox.text)
            {
                transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, 20, false, true);
                textBox.text = resourceManager.GetResource(resourceType).ToString();
            }
        }
    }
}