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
    public Transform m_snapPoint;

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
    }

    public virtual void OnHoverExit()
    {
        Debug.Log("Exited: " + this);
        isHovered = false;
    }

    public virtual void OnSelect()
    {
        isHovered = false;
    }
}
