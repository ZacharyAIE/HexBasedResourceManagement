using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;

namespace ResourceManagement
{

    public class Tile : Clickable
    {
        public ResourceManagement.ResourceManager resourceManager;
        BuildCursor buildCursor;
        public TileType m_tileType;
        public Building m_building;
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
                var b = Instantiate(m_tileType.defaultBuilding.model, m_snapPoint.position, Quaternion.identity);
                b.transform.parent = m_snapPoint; // Separated this from the Instantiate to ignore the scale of the tile.
                
                m_building = m_tileType.defaultBuilding;
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
            if (!m_building && buildCursor.buildingToBuy)
            {
                // BUILDING SUCCESFULLY PLACED!
                if (resourceManager && CanAfford())
                {
                    m_building = buildCursor.buildingToBuy;

                    buildCursor.TakeResources();

                    var nb = Instantiate(m_building.model, m_underTile.position, Quaternion.Euler(0,buildCursor.rotationAngle,0));

                    nb.transform.DOMove(m_snapPoint.position, m_constructAnimLength); // Animate the construction of the building.

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
            if(resourceManager.GetResource(ResourceManager.ResourceType.Gold) >= buildCursor.buildingToBuy.goldCost
                    && resourceManager.GetResource(ResourceManager.ResourceType.Wood) >= buildCursor.buildingToBuy.woodCost
                    && resourceManager.GetResource(ResourceManager.ResourceType.Stone) >= buildCursor.buildingToBuy.stoneCost)
            {
                return true;
            }
            else
                return false;
        }
    }
}