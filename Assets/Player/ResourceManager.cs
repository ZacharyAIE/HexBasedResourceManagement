using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ResourceManagement
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] public int startingWood = 200;
        [SerializeField] public int startingStone = 200;
        [SerializeField] public int startingGold = 100;
        [SerializeField] public int startingPeople = 2;

        [HideInInspector] public List<int> resourceList = new List<int>();
        public ResourceCounterUI resourceCounter;
        public UnityEvent woodChanged;
        public UnityEvent stoneChanged;
        public UnityEvent goldChanged;
        public UnityEvent peopleChanged;
        
        void Awake()
        {
            resourceList.Add(m_woodCount); // 0
            resourceList.Add(m_stoneCount); // 1
            resourceList.Add(m_goldCount); // 2
            resourceList.Add(m_peopleCount); // 3

            m_woodCount = startingWood;
            m_stoneCount = startingStone;
            m_goldCount = startingGold;
            m_peopleCount = startingPeople;

        }

        public void AdjustWoodCount(int amount)
        {
            m_woodCount += amount;
            resourceList[0] = GetWoodCount();
            woodChanged.Invoke();
        }
        public void AdjustPeopleCount(int amount)
        {
            m_peopleCount += amount;
            resourceList[1] = GetPeopleCount();
            peopleChanged.Invoke();
        }
        public void AdjustGoldCount(int amount)
        {
            m_goldCount += amount;
            resourceList[2] = GetGoldCount();
            goldChanged.Invoke();
        }
        public void AdjustStoneCount(int amount)
        {
            m_stoneCount += amount;
            resourceList[3] = GetStoneCount();
            stoneChanged.Invoke();
        }

        public int GetGoldCount()
        {
            return m_goldCount;
        }
        public int GetStoneCount()
        {
            return m_stoneCount;
        }
        public int GetWoodCount()
        {
            return m_woodCount;
        }
        public int GetPeopleCount()
        {
            return m_peopleCount;
        }

        private int m_woodCount;
        private int m_stoneCount;
        private int m_goldCount;
        private int m_peopleCount;
    }
}
