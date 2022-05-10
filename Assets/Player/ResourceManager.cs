using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceManagement
{
    public class ResourceManager : MonoBehaviour
    {
        public List<int> resourceList = new List<int>();
        public ResourceCounterUI resourceCounter;
        
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
        }
        public void AdjustPeopleCount(int amount)
        {
            PeopleAmount += amount;
        }
        public void AdjustGoldCount(int amount)
        {
            GoldAmount += amount;
        }
        public void AdjustStoneCount(int amount)
        {
            StoneAmount += amount;
        }

        private int m_woodCount;
        private int m_stoneCount;
        private int m_goldCount;
        private int m_peopleCount;
    }
}
