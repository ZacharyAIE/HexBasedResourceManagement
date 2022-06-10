using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceManagement;
using DG.Tweening;

namespace ResourceManagement.BuildingSystem
{

    public class ProduceOnTick : MonoBehaviour
    {
        ResourceManager rm;
        public ResourceType resourceToProduce;
        public int amountToProduce;

        // Start is called before the first frame update
        void OnEnable()
        {
            rm = FindObjectOfType<ResourceManager>();
            TickSystem.Instance.OnTick.AddListener(() => Produce());
        }

        void Produce()
        {
            rm.SetResource(resourceToProduce, amountToProduce);
            Animate();
        }

        void Animate()
        {
            gameObject.transform.DOShakeScale(.4f, 0.004f, 1,0,false);
        }
    }
}