using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ResourceManagement.BuildingSystem
{

    public class BuildCursor : MonoBehaviour
    {
        public Building buildingToBuy;
        public ResourceManager resourceManager;
        public GameObject cursorAttachedModel;
        public Material buildPossibleMaterial;
        public Material buildImpossibleMaterial;
        public Material highlightMaterial;

        [HideInInspector] public float rotationAngle;
        public float rotationByAmount = 90;

        Vector2 mousePos;
        Ray ray;
        RaycastHit hit;

        private void Start()
        {
            resourceManager = GetComponent<ResourceManager>();
        }

        private void FixedUpdate()
        {
            // Check if we have a building selected from the UI
            if (buildingToBuy)
            {
                mousePos = Mouse.current.position.ReadValue();
                ray = Camera.main.ScreenPointToRay(mousePos);
                Tile hitObject;

                // If we find a tile
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponentInParent<Tile>())
                {
                    hitObject = hit.collider.gameObject.GetComponentInParent<Tile>();

                    if (!cursorAttachedModel)
                    {
                        // REPLACE THIS WITH OBJECT POOLING THINGOS.
                        cursorAttachedModel = Instantiate(buildingToBuy.model, hit.point, Quaternion.Euler(0, rotationAngle, 0));
                    }
                    else
                    {
                        // Move the model the the new tile's snap point.
                        cursorAttachedModel.transform.position = hitObject.m_snapPoint.position;
                    }

                    // Set the material based on if building is possible
                    if (!hitObject.GetBuildingData() && hitObject.CanAfford())
                    {
                        Material[] mats = cursorAttachedModel.GetComponent<MeshRenderer>().materials;
                        for (int i = 0; i < cursorAttachedModel.GetComponent<MeshRenderer>().materials.Length; i++)
                        {
                            mats[i] = buildPossibleMaterial;
                            cursorAttachedModel.GetComponent<MeshRenderer>().materials = mats;
                        }
                    }
                    else
                    {
                        Material[] mats = cursorAttachedModel.GetComponent<MeshRenderer>().materials;
                        for (int i = 0; i < cursorAttachedModel.GetComponent<MeshRenderer>().materials.Length; i++)
                        {
                            mats[i] = buildImpossibleMaterial;
                            cursorAttachedModel.GetComponent<MeshRenderer>().materials = mats;
                        }
                    }
                }

                // If we dont find a tile then remove the fake object.
                else if (!hit.collider.gameObject.GetComponentInParent<Clickable>())
                {
                    ClearCursor(false);
                }
            }

            // When we dont have a building to buy selected
            else if (!buildingToBuy)
            {
                //// Find whats under mouse
                //mousePos = Mouse.current.position.ReadValue();
                //ray = Camera.main.ScreenPointToRay(mousePos);

                //Tile hitObject;
                //Tile prevHitObject = null;
                //Material[] mats;
                //Material[] oldMatsTemp = null;

                //// If its a tile
                //if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponentInParent<Tile>())
                //{
                //    hitObject = hit.collider.gameObject.GetComponentInParent<Tile>();

                //    if (hitObject)
                //    {
                //        // If the tile has a building on it
                //        if (hitObject != null && hitObject.GetBuilding())
                //        {
                //            mats = hitObject.GetBuilding().GetComponent<MeshRenderer>().materials;
                //            oldMatsTemp = mats;
                //            // Change its material to select it.
                //            for (int i = 0; i < hitObject.GetBuilding().GetComponent<MeshRenderer>().materials.Length; i++)
                //            {
                //                mats[i] = highlightMaterial;
                //                hitObject.GetBuilding().GetComponent<MeshRenderer>().materials = mats;
                //            }
                //            prevHitObject = hitObject;
                //        }
                //        if (prevHitObject != null && prevHitObject != hitObject)
                //        {
                //            if (hitObject.GetBuilding())
                //            {
                //                for (int i = 0; i < hitObject.GetBuilding().GetComponent<MeshRenderer>().materials.Length; i++)
                //                {
                //                    mats = oldMatsTemp;
                //                    prevHitObject.GetBuilding().GetComponent<MeshRenderer>().materials = mats;
                //                }
                //            }
                //        }
                //    }
                    
                //}
            }
        }

        public void SetBuildingToBuy(Building building)
        {
            buildingToBuy = building;
        }

        public void ClearCursor(bool removeBuildingToBuy)
        {
            if(removeBuildingToBuy)
                buildingToBuy = null;

            Destroy(cursorAttachedModel);
            cursorAttachedModel = null;
        }

        public void OnRotate(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && cursorAttachedModel)
            {
                rotationAngle += rotationByAmount;
                cursorAttachedModel.transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
            }
        }

        public void TakeResources()
        {
            DG.Tweening.DOTween.Clear();
            if (buildingToBuy.goldCost > 0)
            {
                resourceManager.SetResource(ResourceManager.ResourceType.Gold, -buildingToBuy.goldCost);
            }
                
            if (buildingToBuy.woodCost > 0)
            {
                resourceManager.SetResource(ResourceManager.ResourceType.Wood, -buildingToBuy.woodCost);
            }
                
            if (buildingToBuy.stoneCost > 0)
            {
                resourceManager.SetResource(ResourceManager.ResourceType.Stone, -buildingToBuy.stoneCost);
            }
        }
    }
}