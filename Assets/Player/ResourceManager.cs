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
            resourceList.Add(WoodAmount);
            resourceList.Add(StoneAmount);
            resourceList.Add(GoldAmount);
            resourceList.Add(PeopleAmount);
        }

        public int WoodAmount { 
            get
            {
                return m_woodCount;
            }
            set
            {
                m_woodCount = value;
                resourceCounter.UpdateUI();
            }
        }
        public int StoneAmount { 
            get
            {
                return m_stoneCount;
            }
            set
            {
                m_stoneCount = value;
                resourceCounter.UpdateUI();
            } 
        }
        public int GoldAmount { 
            get
            {
                return m_goldCount;
            }
            set
            {
                m_goldCount = value;
                resourceCounter.UpdateUI();
            }
        }
        public int PeopleAmount { 
            get
            {
                return m_peopleCount;
            }
            set
            {
                m_peopleCount = value;
                resourceCounter.UpdateUI();
            }
        }

        public void AdjustWoodCount(int amount)
        {
            WoodAmount += amount;
            woodChanged.Invoke();
        }
        public void AdjustPeopleCount(int amount)
        {
            PeopleAmount += amount;
            peopleChanged.Invoke();
        }
        public void AdjustGoldCount(int amount)
        {
            GoldAmount += amount;
            goldChanged.Invoke();
        }
        public void AdjustStoneCount(int amount)
        {
            StoneAmount += amount;
            stoneChanged.Invoke();
        }

        private int m_woodCount = 200;
        private int m_stoneCount = 200;
        private int m_goldCount = 200;
        private int m_peopleCount = 5;
    }
}
