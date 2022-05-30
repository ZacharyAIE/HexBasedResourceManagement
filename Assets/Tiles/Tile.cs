using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ResourceManagement
{

    public class Tile : Clickable
    {
        public ResourceManagement.ResourceManager resourceManager;
        BuildCursor buildingManager;
        public TileType m_tileType;
        public Building m_building;
        


        private void Start()
        {
            buildingManager = FindObjectOfType<BuildCursor>();
            resourceManager = FindObjectOfType<ResourceManager>();
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
                if (resourceManager && resourceManager.GetGoldCount() >= buildingManager.buildingToBuy.cost)
                {
                    m_building = buildingManager.buildingToBuy;
                    resourceManager.AdjustGoldCount(-m_building.cost);
                    Instantiate(m_building.model, m_snapPoint.position, Quaternion.Euler(0,buildingManager.rotationAngle,0));

                }

                else
                {
                    Debug.Log("Insufficient Gold");
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