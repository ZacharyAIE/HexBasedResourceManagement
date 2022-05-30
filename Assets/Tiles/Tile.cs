using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace ResourceManagement
{

    public class Tile : Clickable
    {
        public ResourceManagement.ResourceManager resourceManager;
        BuildCursor buildingManager;
        public TileType m_tileType;
        public Building m_building;
        AudioSource m_audioSource;
        public AudioClip buildingPlaceSound;

        private void Start()
        {
            buildingManager = FindObjectOfType<BuildCursor>();
            resourceManager = FindObjectOfType<ResourceManager>();
            m_audioSource = GetComponent<AudioSource>();
            Instantiate(m_tileType.model, transform, false);
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
            if (!m_building && buildingManager.buildingToBuy)
            {
                // BUILDING SUCCESFULLY PLACED!
                if (resourceManager && resourceManager.GetGoldCount() >= buildingManager.buildingToBuy.cost)
                {
                    m_building = buildingManager.buildingToBuy;
                    resourceManager.AdjustGoldCount(-m_building.cost);
                    Instantiate(m_building.model, m_snapPoint.position, Quaternion.Euler(0,buildingManager.rotationAngle,0));

                    m_audioSource.PlayOneShot(buildingPlaceSound);

                    buildingManager.ClearCursor(true);
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

                //GameObject go = Resources.Load("Hex") as Mesh;
                //hexMesh = go.GetComponent<MeshRenderer>();

                hexMesh = Resources.Load<Mesh>("Hex");
            }
            return hexMesh;
        }

        
    }
}