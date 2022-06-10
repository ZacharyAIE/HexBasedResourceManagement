using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;
using ResourceManagement.BuildingSystem;

namespace ResourceManagement.Tiles
{
    public class Tile : Clickable
    {
        public ResourceManagement.ResourceManager resourceManager;
        BuildCursor buildCursor;
        public TileType m_tileType;
        private Building m_buildingData;
        private GameObject m_currentBuilding;
        AudioSource m_audioSource;
        public AudioClip buildingPlaceSound;
        public ParticleSystem m_particleSystem;
        public Transform m_underTile;
        public float m_constructAnimLength = 0.7f;
        

        private void Start()
        {
            buildCursor = FindObjectOfType<BuildCursor>();
            resourceManager = FindObjectOfType<ResourceManager>();
            m_audioSource = GetComponent<AudioSource>();
            m_particleSystem = GetComponentInChildren<ParticleSystem>();
            Instantiate(m_tileType.model, transform, false);

            if (m_tileType.defaultBuilding)
            {
                m_currentBuilding = Instantiate(m_tileType.defaultBuilding.model, m_snapPoint.position, Quaternion.identity);
                m_currentBuilding.transform.parent = m_snapPoint; // Separated this from the Instantiate to ignore the scale of the tile.
                
                m_buildingData = m_tileType.defaultBuilding;
            }
        }

        public override void OnHoverEnter()
        {
            base.OnHoverEnter();
        }

        public override void OnHoverExit()
        {
            base.OnHoverExit();
        }

        public override void OnSelect()
        {
            if (!m_buildingData && buildCursor.buildingToBuy && buildCursor.BuildingEnabled())
            {
                // BUILDING SUCCESFULLY PLACED!
                if (resourceManager && CanAfford())
                {
                    m_buildingData = buildCursor.buildingToBuy;

                    buildCursor.TakeResources();

                    m_currentBuilding = Instantiate(m_buildingData.model, m_underTile.position, Quaternion.Euler(0,buildCursor.rotationAngle,0));

                    // Set the building parent
                    m_currentBuilding.transform.SetParent(m_snapPoint, true);

                    // Animate the construction of the building.
                    m_currentBuilding.transform.DOMove(m_snapPoint.position, m_constructAnimLength); 

                    if(m_buildingData?.resourceToProduce != ResourceType.None)
                    {
                        m_currentBuilding.AddComponent<ProduceOnTick>();
                        ProduceOnTick producer = m_currentBuilding.GetComponent<ProduceOnTick>();
                        producer.amountToProduce = m_buildingData.amountToProduce;
                        producer.resourceToProduce = m_buildingData.resourceToProduce;
                        producer.produceSound = m_buildingData.produceSound;
                    }

                    var tileColour = m_tileType.model.GetComponent<MeshRenderer>().sharedMaterials[1].color;
                    m_particleSystem.GetComponent<ParticleSystemRenderer>().material.color = new Color(tileColour.r /2, tileColour.g /2, tileColour.b /2, tileColour.a);

                    m_particleSystem.Play();

                    m_audioSource.PlayOneShot(buildingPlaceSound);

                    buildCursor.ClearCursor(true);
                    
                }

                else
                {
                    resourceManager.resourceCounter.BuildFailed();
                }
            }
            if (m_buildingData && !buildCursor.buildingToBuy)
            {

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1,0,1,0.5f);
            Gizmos.DrawMesh(GetHexMesh(), transform.position, transform.rotation, transform.lossyScale);
        }

        private static Mesh hexMesh;
        Mesh GetHexMesh()
        {
            if (!hexMesh)
            {
                hexMesh = Resources.Load<Mesh>("Hex");
            }
            return hexMesh;
        }

        public bool CanAfford()
        {
            foreach (ResourceType rt in System.Enum.GetValues(typeof(ResourceType)))
            {
                if(rt != ResourceType.None)
                {
                    if (buildCursor.buildingToBuy.GetCost(rt) > resourceManager.GetResource(rt))
                                    {
                                        return false;
                                    }
                }
            }
            return true;
        }

        public Building GetBuildingData()
        {
            return m_buildingData;
        }

        public GameObject GetBuilding()
        {
            return m_currentBuilding;
        }
    }
}