using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ResourceManagement
{
    public class ResourceManager : MonoBehaviour
    {
        public List<int> resourceList = new List<int>();
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

        private int m_woodCount = 200;
        private int m_stoneCount = 200;
        private int m_goldCount = 200;
        private int m_peopleCount = 5;
    }
}
