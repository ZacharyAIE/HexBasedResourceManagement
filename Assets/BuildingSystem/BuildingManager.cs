using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Building buildingToBuy;
    public GameObject cursorAttachedModel;

    Vector2 mousePos;
    Ray ray;
    RaycastHit hit;

    private void FixedUpdate()
    {
        // Check if we have a building selected from the UI
        if(buildingToBuy)
        {
            mousePos = Mouse.current.position.ReadValue();
            ray = Camera.main.ScreenPointToRay(mousePos);
            Clickable hitObject;

            // If we find a tile
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<Clickable>())
            {
                hitObject = hit.collider.gameObject.GetComponent<Clickable>();
                if (!cursorAttachedModel)
                {
                    // REPLACE THIS WITH OBJECT POOLING THINGOS.
                    cursorAttachedModel = Instantiate(buildingToBuy.model, hit.point, Quaternion.identity);
                }
                else
                {
                    // Move the model the the new tile's snap point.
                    cursorAttachedModel.transform.position = hitObject.m_snapPoint.position;
                }

            }

            // If we dont find a tile then remove the fake object.
            else if (!hit.collider.gameObject.GetComponent<Clickable>())
            {
                Destroy(cursorAttachedModel);
                cursorAttachedModel = null;
            }
        }  
    }

    public void SetBuildingToBuy(Building building)
    {
        buildingToBuy = building;
    }
}
