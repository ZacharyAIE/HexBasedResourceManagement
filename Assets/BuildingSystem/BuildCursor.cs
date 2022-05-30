using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ResourceManagement
{

    public class BuildCursor : MonoBehaviour
    {
        public Building buildingToBuy;
        public GameObject cursorAttachedModel;
        [HideInInspector] public float rotationAngle;
        public float rotationByAmount = 90;

        Vector2 mousePos;
        Ray ray;
        RaycastHit hit;

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
                    if (!hitObject.m_building)
                    {
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
                    }
                    else
                        ClearCursor(false);
                }

                // If we dont find a tile then remove the fake object.
                else if (!hit.collider.gameObject.GetComponentInParent<Clickable>())
                {
                    ClearCursor(false);
                }
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
    }
}