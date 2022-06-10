using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ResourceManagement;
using DG.Tweening;

namespace ResourceManagement.BuildingSystem
{

    public class ProduceOnTick : MonoBehaviour
    {
        ResourceManager rm;
        public ResourceType resourceToProduce;
        public int amountToProduce;
        public AudioClip produceSound;
        public AudioSource audioSource;
        //public UnityEvent OnProduce;

        void OnEnable()
        {
            rm = FindObjectOfType<ResourceManager>();
            TickSystem.Instance.OnTick.AddListener(() => Produce());
            audioSource = GetComponentInParent<AudioSource>();
        }

        void Produce()
        {
            rm.SetResource(resourceToProduce, amountToProduce);
            Animate();
            if(produceSound && audioSource)
            {
                audioSource.PlayOneShot(produceSound);
            }
        }

        void Animate()
        {
            gameObject.transform.DOShakeScale(0.4f, 0.004f, 1, 0, false);
        }
    }
}