using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using ResourceManagement;

public class ScreenToWorldPicker : MonoBehaviour
{
    BuildCursor buildingManager;
    Clickable selectedObject;
    Clickable m_hoveredObject;
    Clickable HoveredObject 
    {
        get { return m_hoveredObject; } 

        set {
            if(m_hoveredObject != null)
            {
                m_hoveredObject.OnHoverExit();
            }
            m_hoveredObject = value;
            if (m_hoveredObject != null)
                m_hoveredObject.OnHoverEnter();
        } 
    }
    public Vector2 mousePos;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        buildingManager = FindObjectOfType<BuildCursor>();
    }

    private void FixedUpdate()
    {
        mousePos = Mouse.current.position.ReadValue();
        ray = Camera.main.ScreenPointToRay(mousePos);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<Clickable>() && !buildingManager.buildingToBuy)
            {
                Clickable obj = hit.collider.GetComponent<Clickable>();

                if(!obj.isHovered && !obj.isSelected)
                {
                    if(HoveredObject == null)
                        HoveredObject = obj;
                    else
                    {
                        HoveredObject = obj;
                    }
                }

                else if(!obj.isHovered && HoveredObject != obj)
                {
                    HoveredObject = obj;
                }

                else if(!obj.isHovered && HoveredObject == obj && obj.isSelected)
                {
                    return;
                }

                else
                {
                    return;
                }
            }
            else
            {
                if (HoveredObject != null)
                {
                    HoveredObject = null;
                }
            }
        }

        else
        {
            if(HoveredObject != null)
            {
                HoveredObject = null;
            }
            
        }
    }

    public void ScreenClick(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (ctx.canceled)
        {

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponentInParent<Clickable>())
                {
                    if (selectedObject == this)
                    {
                        selectedObject = null;
                    }
                    else
                    {
                        selectedObject = hit.collider.GetComponentInParent<Clickable>();
                        selectedObject.OnSelect();
                    }
                }
            }
        }
    }
}
