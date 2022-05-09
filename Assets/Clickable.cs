using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Clickable : MonoBehaviour
{
    public MeshRenderer m_renderer;
    public Color m_highlightColour;
    public Color m_selectedColour;
    public bool isHovered = false;
    public bool isSelected = false;

    public List<Color> m_defaultColours;

    public virtual void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();

        foreach (Material mat in m_renderer.materials)
        {
            m_defaultColours.Add(mat.color);
        }
    }

    public virtual void OnHoverEnter()
    {
        isHovered = true;
        Debug.Log("Highlighted: " + this);
        foreach (Material mat in m_renderer.materials)
        {
            mat.color = m_highlightColour;
        }
    }

    public virtual void OnHoverExit()
    {
        Debug.Log("Exited: " + this);
        isHovered = false;
        ResetMaterials();
    }

    public virtual void OnSelect()
    {
        isHovered = false;
        Debug.Log("Selected: " + this);
        foreach (Material mat in m_renderer.materials)
        {
            mat.color = m_selectedColour;
        }
    }

    [ContextMenu("Reset")]
    public void ResetMaterials()
    {
        for (int i = 0; i < m_renderer.materials.Length; i++)
        {
            m_renderer.materials[i].color = m_defaultColours[i];
        }
    }
}
