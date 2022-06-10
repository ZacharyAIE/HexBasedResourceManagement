using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceManagement;

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
        }
    }
}